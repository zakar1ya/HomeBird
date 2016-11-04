using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.Ef6.Context;
using HomeBird.DataBase.Ef6.Models;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Logic
{
    public class SalesUnit : ISalesUnit
    {
        private HomeBirdContext _dc;
        private IMapper _mapper;

        public SalesUnit(HomeBirdContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task<HbResult<HbSale>> Create(CreateSaleForm form)
        {
            var lotExist = await _dc.Lots.Where(u => !u.IsDeleted && u.Id == form.LotId).AnyAsync();
            if (!lotExist)
                return new HbResult<HbSale>(ErrorCodes.LotNotFound);

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

            return new HbResult<HbSale>(_mapper.Map<HbSale>(sale));
        }

        public async Task<HbResult<HbSale>> Update(UpdateSaleForm form)
        {
            var lotExist = await _dc.Lots.Where(u => !u.IsDeleted && u.Id == form.LotId).AnyAsync();
            if (!lotExist)
                return new HbResult<HbSale>(ErrorCodes.LotNotFound);

            var sale = await _dc.Sales.FirstOrDefaultAsync(u => u.Id == form.Id && !u.IsDeleted);
            if (sale == null)
                return new HbResult<HbSale>(ErrorCodes.SaleNotFound);

            sale.SaleDate = form.SaleDate;
            sale.Comment = form.Comment;
            sale.Count = form.Count;
            sale.Amount = form.Amount;
            sale.Buyer = form.Buyer;
            sale.Type = form.Type;
            sale.LotId = form.LotId;

            await _dc.SaveChangesAsync();

            return new HbResult<HbSale>(_mapper.Map<HbSale>(sale));
        }

        public async Task<HbResult<HbSale>> GetById(int id)
        {
            var sale = await _dc.Sales.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id);
            if (sale == null)
                return new HbResult<HbSale>(ErrorCodes.SaleNotFound);

            return new HbResult<HbSale>(_mapper.Map<HbSale>(sale));
        }

        public async Task<IEnumerable<HbSale>> GetList(PagedSalesForm form)
        {
            var query = _dc.Sales.Where(u => !u.IsDeleted)
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
        }
    }
}
