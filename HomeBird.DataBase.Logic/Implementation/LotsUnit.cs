using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.EfCore.Context;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HomeBird.DataBase.EfCore.Models;

namespace HomeBird.DataBase.Logic
{
    internal class LotsUnit : ILotsUnit
    {
        private HomeBirdContext _dc;
        private IMapper _mapper;

        public LotsUnit(HomeBirdContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task<int> Count(PagedLotsForm form)
        {
            return await _dc.Lots
                            .Where(u => u.CreationDate > form.Start && u.CreationDate < form.End)
                            .Where(u => !u.IsDeleted)
                            .CountAsync();
        }

        public async Task<HbResult<HbLot>> Create(CreateLotForm form)
        {
            var exist = await _dc.Lots.AnyAsync(u => u.Identifier == form.IdentifierNumber && u.Year == form.Year && !u.IsDeleted);
            if (exist)
                return new HbResult<HbLot>(ErrorCodes.LotAlreadyExist);

            var dbLot = _dc.Lots.Add(new HbLots
            {
                Identifier = form.IdentifierNumber,
                CreationDate = DateTime.UtcNow,
                Year = form.Year
            });

            await _dc.SaveChangesAsync();

            return new HbResult<HbLot>(_mapper.Map<HbLot>(dbLot.Entity));
        }

        public async Task<HbResult<HbLot>> Update(UpdateLotForm form)
        {
            var lot = await _dc.Lots.FirstOrDefaultAsync(u => u.Id == form.Id && !u.IsDeleted);
            if (lot == null)
                return new HbResult<HbLot>(ErrorCodes.LotNotFound);

            lot.Identifier = form.IdentifierNumber;

            await _dc.SaveChangesAsync();

            return new HbResult<HbLot>(_mapper.Map<HbLot>(lot));
        }

        public async Task Delete(int lotId)
        {
            var lot = await _dc.Lots
                               .Include(u => u.Sales)
                               .Include(u => u.Purchases)
                               .Include(u => u.Overheads)
                               .Include(u => u.Layings)
                               .Include(u => u.Broods)
                               .FirstOrDefaultAsync(u => u.Id == lotId && !u.IsDeleted);

            if (lot == null)
                return;

            lot.IsDeleted = true;

            foreach (var brood in lot.Broods)// TODO: it's necessary?
                brood.IsDeleted = true;
            foreach (var sale in lot.Sales)
                sale.IsDeleted = true;
            foreach (var purchase in lot.Purchases)
                purchase.IsDeleted = true;
            foreach (var laying in lot.Layings)
                laying.IsDeleted = true;
            foreach (var overhead in lot.Overheads)
                overhead.IsDeleted = true;

            await _dc.SaveChangesAsync();
        }

        public async Task<IEnumerable<HbLot>> GetList(PagedLotsForm form)
        {
            var lots = await _dc.Lots
                                .Where(u => u.Year == form.Year)
                                .Where(u => !u.IsDeleted)
                                .OrderByDescending(u => u.Id)
                                .Skip(form.Offset)
                                .Take(form.Count)
                                .ToArrayAsync();

            return lots.Select(_mapper.Map<HbLot>).ToArray();
        }

        public async Task<HbResult<HbLot>> GetById(int lotId)
        {
            var lot = await _dc.Lots.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == lotId);
            if (lot == null)
                return new HbResult<HbLot>(ErrorCodes.LotNotFound);

            return new HbResult<HbLot>(_mapper.Map<HbLot>(lot));
        }

        public async Task RecalculateLot(int id)
        {
            var lot = await _dc.Lots.Include(u => u.Broods)
                                    .Include(u => u.Layings)
                                    .Include(u => u.Overheads)
                                    .Include(u => u.Purchases)
                                    .Include(u => u.Sales)
                                    .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);            

            var overheads = lot.Overheads.Where(u => !u.IsDeleted);
            var salesAdults = lot.Sales.Where(u => u.Type == SalesTypes.AdultChicken && !u.IsDeleted);
            var salesDaily = lot.Sales.Where(u => u.Type == SalesTypes.DailyChicken && !u.IsDeleted);
            var salesAll = lot.Sales.Where(u => !u.IsDeleted);
            var layings = lot.Layings.Where(u => !u.IsDeleted);
            var broods = lot.Broods.Where(u => !u.IsDeleted);
            var purchases = lot.Purchases.Where(u => !u.IsDeleted);

            if (salesDaily.Any())
                lot.AvgDailyPrice = Math.Round(salesDaily.Average(u => u.Count / u.Amount), 2);

            if (salesAdults.Any())
                lot.AvgAdultPrice = Math.Round(salesAdults.Average(u => u.Count / u.Amount), 2);

            if (layings.Any() && broods.Any())
                lot.Loses = layings.Sum(u => u.Count) - broods.Sum(u => u.Count);

            if (salesAll.Any())
                lot.Profit = salesAll.Sum(u => u.Amount) - (overheads.Select(u => u.Amount).DefaultIfEmpty(0).Sum() + purchases.Select(u => u.Amount).DefaultIfEmpty(0).Sum());

            lot.SoldCount = salesDaily.Select(u => u.Count).DefaultIfEmpty(0).Sum() + salesAdults.Select(u => u.Count).DefaultIfEmpty(0).Sum();

            lot.EggPrice = Math.Round(purchases.Sum(u => u.Amount) / layings.Sum(u => u.Count), 2);

            await _dc.SaveChangesAsync();
        }
    }
}
