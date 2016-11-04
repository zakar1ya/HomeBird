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
    public class BroodsUnit : IBroodsUnit
    {
        private readonly HomeBirdContext _dc;
        private readonly IMapper _mapper;

        public BroodsUnit(HomeBirdContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
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

            var layingSum = lot.Layings.Where(u => !u.IsDeleted).Sum(u => u.Count);
            var broodPrice = lot.Overheads.Where(u => !u.IsDeleted).Sum(u => u.Amount) + lot.Purchases.Where(u => !u.IsDeleted).Sum(u => u.Amount);

            var brood = _dc.Broods.Add(new HbBroods
            {
                BroodDate = form.BroodDate,
                Count = form.Count,
                DeadCount = form.DeadCount,
                EmptyCount = form.EmptyCount,
                LotId = form.LotId,
                DeadPercent = 100 * form.DeadCount / layingSum,
                EmptyPercent = 100 * form.EmptyCount / layingSum,
                Percent = 100 * form.Count / layingSum,
                PlacePrice = broodPrice / form.Count
            });

            await _dc.SaveChangesAsync();

            return new HbResult<HbBrood>(_mapper.Map<HbBrood>(brood));
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

            var layingSum = lot.Layings.Where(u => !u.IsDeleted).Sum(u => u.Count);
            var broodPrice = lot.Overheads.Where(u => !u.IsDeleted).Sum(u => u.Amount) + lot.Purchases.Where(u => !u.IsDeleted).Sum(u => u.Amount);

            brood.BroodDate = form.BroodDate;
            brood.Count = form.Count;
            brood.DeadCount = form.DeadCount;
            brood.EmptyCount = form.EmptyCount;
            brood.LotId = form.LotId;
            brood.DeadPercent = 100 * form.DeadCount / layingSum;
            brood.EmptyPercent = 100 * form.EmptyCount / layingSum;
            brood.Percent = 100 * form.Count / layingSum;
            brood.PlacePrice = broodPrice / form.Count;

            await _dc.SaveChangesAsync();

            return new HbResult<HbBrood>(_mapper.Map<HbBrood>(brood));
        }

        public async Task Delete(int id)
        {
            var brood = await _dc.Broods.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id);
            if (brood == null)
                return;

            brood.IsDeleted = true;

            await _dc.SaveChangesAsync();
        }

        public async Task<HbResult<HbBrood>> GetById(int id)
        {
            var brood = await _dc.Broods.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == id);
            if (brood == null)
                return new HbResult<HbBrood>(ErrorCodes.BroodNotFound);

            return new HbResult<HbBrood>(_mapper.Map<HbBrood>(brood));
        }

        public async Task<IEnumerable<HbBrood>> GetList(PagedBroodsForm form)
        {
            var query = _dc.Broods
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
