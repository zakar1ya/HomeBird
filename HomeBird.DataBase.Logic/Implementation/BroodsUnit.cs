using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.EfCore.Context;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HomeBird.DataBase.EfCore.Models;
using System;

namespace HomeBird.DataBase.Logic
{
    internal class BroodsUnit : IBroodsUnit
    {
        private readonly HomeBirdContext _dc;
        private readonly IMapper _mapper;
        private readonly ILotsUnit _lotsUnit;

        public BroodsUnit(HomeBirdContext dc, IMapper mapper, ILotsUnit lotsUnit)
        {
            _dc = dc;
            _mapper = mapper;
            _lotsUnit = lotsUnit;
        }

        public async Task<int> Count(PagedBroodsForm form)
        {
            var query = _dc.Broods
               .Where(u => !u.IsDeleted)
               .Where(u => u.BroodDate > form.Start && u.BroodDate < form.End.Value)
               .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId.Value);

            return await query.CountAsync();
        }

        public async Task<HbResult<HbBrood>> Create(CreateBroodForm form)
        {
            var lot = await _dc.Lots
                               .Include(u => u.Layings)
                               .Include(u => u.Overheads)
                               .Include(u => u.Purchases)
                               .FirstOrDefaultAsync(u => u.Id == form.LotId && !u.IsDeleted);
            if (lot == null)
                return new HbResult<HbBrood>(ErrorCodes.LotNotFound);

            var existBroodsSum = await _dc.Broods.Where(u => u.LotId == form.LotId && !u.IsDeleted)
                                                 .SumAsync(u => u.Count);

            var layingSum = lot.Layings.Where(u => !u.IsDeleted).Sum(u => u.Count);
            if (layingSum < existBroodsSum + form.Count)
                return new HbResult<HbBrood>(ErrorCodes.BroodAmountMoreThanLayingsSum);

            var broodPrice = lot.Overheads.Where(u => !u.IsDeleted).Sum(u => u.Amount) + lot.Purchases.Where(u => !u.IsDeleted).Sum(u => u.Amount);

            var brood = _dc.Broods.Add(new HbBroods
            {
                CreationDate = DateTimeOffset.UtcNow,
                BroodDate = form.BroodDate,
                Count = form.Count,
                DeadCount = form.DeadCount,
                EmptyCount = form.EmptyCount,
                LotId = form.LotId,
                DeadPercent = Math.Round(100m * form.DeadCount / layingSum, 2),
                EmptyPercent = Math.Round(100m * form.EmptyCount / layingSum, 2),
                Percent = Math.Round(100m * form.Count / layingSum, 2),
                PlacePrice = Math.Round(broodPrice / form.Count, 2)
            });

            await _dc.SaveChangesAsync();

            await _lotsUnit.RecalculateLot(form.LotId);

            return new HbResult<HbBrood>(_mapper.Map<HbBrood>(brood.Entity));
        }

        public async Task<HbResult<HbBrood>> Update(UpdateBroodForm form)
        {
            var brood = await _dc.Broods.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == form.Id);
            if (brood == null)
                return new HbResult<HbBrood>(ErrorCodes.BroodNotFound);

            var lot = await _dc.Lots
                               .Include(u => u.Layings)
                               .Include(u => u.Overheads)
                               .Include(u => u.Purchases)
                               .FirstOrDefaultAsync(u => u.Id == form.LotId && !u.IsDeleted);
            if (lot == null)
                return new HbResult<HbBrood>(ErrorCodes.LotNotFound);

            var existBroodsSum = await _dc.Broods.Where(u => u.LotId == form.LotId && u.Id != form.Id && !u.IsDeleted)
                                                 .SumAsync(u => u.Count);

            var layingSum = lot.Layings.Where(u => !u.IsDeleted).Sum(u => u.Count);
            if (layingSum < existBroodsSum + form.Count)
                return new HbResult<HbBrood>(ErrorCodes.BroodAmountMoreThanLayingsSum);

            var broodPrice = lot.Overheads.Where(u => !u.IsDeleted).Sum(u => u.Amount) + lot.Purchases.Where(u => !u.IsDeleted).Sum(u => u.Amount);

            brood.BroodDate = form.BroodDate;
            brood.Count = form.Count;
            brood.DeadCount = form.DeadCount;
            brood.EmptyCount = form.EmptyCount;
            brood.LotId = form.LotId;
            brood.DeadPercent = Math.Round(100m * form.DeadCount / layingSum, 2);
            brood.EmptyPercent = Math.Round(100m * form.EmptyCount / layingSum, 2);
            brood.Percent = Math.Round(100m * form.Count / layingSum, 2);
            brood.PlacePrice = Math.Round(broodPrice / form.Count, 2);

            await _dc.SaveChangesAsync();

            await _lotsUnit.RecalculateLot(brood.LotId);

            return new HbResult<HbBrood>(_mapper.Map<HbBrood>(brood));
        }

        public async Task Remove(int id)
        {
            var brood = await _dc.Broods.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id);
            if (brood == null)
                return;

            brood.IsDeleted = true;

            await _dc.SaveChangesAsync();

            await _lotsUnit.RecalculateLot(brood.LotId);
        }

        public async Task<HbResult<HbBrood>> GetById(int id)
        {
            var brood = await _dc.Broods.Include(u => u.Lot)
                                        .FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id);
            if (brood == null)
                return new HbResult<HbBrood>(ErrorCodes.BroodNotFound);

            return new HbResult<HbBrood>(_mapper.Map<HbBrood>(brood));
        }

        public async Task<IEnumerable<HbBrood>> GetList(PagedBroodsForm form)
        {
            var query = _dc.Broods.Include(u => u.Lot)
                                  .Where(u => !u.IsDeleted)
                                  .Where(u => u.BroodDate > form.Start && u.BroodDate < form.End.Value)
                                  .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId.Value);

            var broods = await query.OrderByDescending(u => u.Id)
                                    .Skip(form.Offset)
                                    .Take(form.Count)
                                    .ToArrayAsync();

            return broods.Select(_mapper.Map<HbBrood>).ToArray();
        }
    }
}
