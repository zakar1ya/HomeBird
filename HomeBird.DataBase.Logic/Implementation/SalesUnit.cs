using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.EfCore.Context;
using HomeBird.DataBase.EfCore.Models;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Logic
{
    internal class SalesUnit : ISalesUnit
    {
        private readonly HomeBirdContext _dc;
        private readonly IMapper _mapper;
        private readonly ILotsUnit _lotsUnit;

        public SalesUnit(HomeBirdContext dc, IMapper mapper, ILotsUnit lotsUnit)
        {
            _dc = dc;
            _mapper = mapper;
            _lotsUnit = lotsUnit;
        }

        public async Task<int> Count(PagedSalesForm form)
        {
            var query = _dc.Sales.Where(u => !u.IsDeleted)
                     .Where(u => u.SaleDate > form.Start && u.SaleDate < form.End)
                     .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId);
            if (form.Type.HasValue)
                query = query.Where(u => u.Type == form.Type);

            return await query.CountAsync();
        }

        public async Task<HbResult<HbSale>> Create(CreateSaleForm form)
        {
            var lotExist = await _dc.Lots.Where(u => !u.IsDeleted && u.Id == form.LotId).AnyAsync();
            if (!lotExist)
                return new HbResult<HbSale>(ErrorCodes.LotNotFound);

            if(form.Type != SalesTypes.Egg)
            {
                var soldChickens = await _dc.Sales.Where(u => u.LotId == form.LotId && u.Type != SalesTypes.Egg && !u.IsDeleted).SumAsync(u => u.Count);
                var broodCount = await _dc.Broods.Where(u => u.LotId == form.LotId && !u.IsDeleted).SumAsync(u => u.Count);

                if (form.Count > broodCount - soldChickens)
                    return new HbResult<HbSale>(ErrorCodes.SalesCountMoreThanBroodCount);
            }

            var sale = _dc.Sales.Add(new HbSales
            {
                Amount = form.Amount,
                Buyer = form.Buyer,
                Comment = form.Comment,
                Count = form.Count,
                LotId = form.LotId,
                SaleDate = form.SaleDate,
                Type = form.Type
            });

            await _dc.SaveChangesAsync();

            await _lotsUnit.RecalculateLot(form.LotId);

            return new HbResult<HbSale>(_mapper.Map<HbSale>(sale.Entity));
        }

        public async Task<HbResult<HbSale>> Update(UpdateSaleForm form)
        {
            var lotExist = await _dc.Lots.Where(u => !u.IsDeleted && u.Id == form.LotId).AnyAsync();
            if (!lotExist)
                return new HbResult<HbSale>(ErrorCodes.LotNotFound);

            var sale = await _dc.Sales.FirstOrDefaultAsync(u => u.Id == form.Id && !u.IsDeleted);
            if (sale == null)
                return new HbResult<HbSale>(ErrorCodes.SaleNotFound);

            if (form.Type != SalesTypes.Egg)
            {
                var soldChickens = await _dc.Sales.Where(u => u.LotId == form.LotId && u.Id != form.Id && u.Type != SalesTypes.Egg && !u.IsDeleted).SumAsync(u => u.Count);
                var broodCount = await _dc.Broods.Where(u => u.LotId == form.LotId && !u.IsDeleted).SumAsync(u => u.Count);

                if (form.Count > broodCount - soldChickens)
                    return new HbResult<HbSale>(ErrorCodes.SalesCountMoreThanBroodCount);
            }

            sale.SaleDate = form.SaleDate;
            sale.Comment = form.Comment;
            sale.Count = form.Count;
            sale.Amount = form.Amount;
            sale.Buyer = form.Buyer;
            sale.Type = form.Type;
            sale.LotId = form.LotId;

            await _dc.SaveChangesAsync();

            await _lotsUnit.RecalculateLot(form.LotId);

            return new HbResult<HbSale>(_mapper.Map<HbSale>(sale));
        }

        public async Task<HbResult<HbSale>> GetById(int id)
        {
            var sale = await _dc.Sales.Include(u => u.Lot)
                                      .FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id);
            if (sale == null)
                return new HbResult<HbSale>(ErrorCodes.SaleNotFound);

            return new HbResult<HbSale>(_mapper.Map<HbSale>(sale));
        }

        public async Task<IEnumerable<HbSale>> GetList(PagedSalesForm form)
        {
            var query = _dc.Sales.Include(u => u.Lot)
                                 .Where(u => !u.IsDeleted)
                                 .Where(u => u.SaleDate > form.Start && u.SaleDate < form.End)
                                 .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId);
            if (form.Type.HasValue)
                query = query.Where(u => u.Type == form.Type);

            var sales = await query.OrderByDescending(u => u.Id)
                                   .Skip(form.Offset)
                                   .Take(form.Count)
                                   .ToArrayAsync();

            return sales.Select(_mapper.Map<HbSale>).ToArray();
        }

        public async Task Delete(int id)
        {
            var sale = await _dc.Sales.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id);
            if (sale == null)
                return;

            sale.IsDeleted = true;

            await _dc.SaveChangesAsync();

            await _lotsUnit.RecalculateLot(sale.LotId);
        }
    }
}
