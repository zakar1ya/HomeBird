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
    public class LayingsUnit : ILayingsUnit
    {
        private readonly HomeBirdContext _dc;
        private readonly IMapper _mapper;

        public LayingsUnit(HomeBirdContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task<int> Count(PagedLayingsForm form)
        {
            var query = _dc.Layings.Where(u => !u.IsDeleted)
                       .Where(u => u.CreationTime > form.Start && u.CreationTime < form.End)
                       .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId);
            if (form.IncubatorId.HasValue)
                query = query.Where(u => u.IncubatorId == form.IncubatorId);

            return await query.CountAsync();
        }

        public async Task<HbResult<HbLaying>> Create(CreateLayingForm form)
        {
            var lotExist = await _dc.Lots.Where(u => !u.IsDeleted && u.Id == form.LotId).AnyAsync();
            if (!lotExist)
                return new HbResult<HbLaying>(ErrorCodes.LotNotFound);

            var incubatorExist = await _dc.Incubators.AnyAsync(u => !u.IsDeleted && u.Id == form.IncubatorId);
            if (!incubatorExist)
                return new HbResult<HbLaying>(ErrorCodes.IncubatorNotFound);

            var overheads = await _dc.Overheads
                                     .Where(u => !u.IsDeleted && u.LotId == form.LotId)
                                     .SumAsync(u => u.Amount);

            var purchases = await _dc.Purchases
                                     .Where(u => !u.IsDeleted && u.LotId == form.LotId)
                                     .SumAsync(u => u.Amount);

            var layings = await _dc.Layings.Where(u => !u.IsDeleted)
                                           .Where(u => u.LotId == form.LotId)
                                           .ToArrayAsync();

            var eggPrice = (overheads + purchases) / (form.Count + layings.Sum(u => u.Count));

            var laying = _dc.Layings.Add(new HbLayings
            {
                Count = form.Count,
                CreationTime = form.CreationDate,
                LotId = form.LotId,
                IncubatorId = form.IncubatorId,
                EggPrice = eggPrice
            });

            foreach (var item in layings)
                item.EggPrice = eggPrice;

            await _dc.SaveChangesAsync();

            return new HbResult<HbLaying>(_mapper.Map<HbLaying>(laying));
        }

        public async Task Delete(int id)
        {
            var laying = await _dc.Layings.Where(u => u.Id == id && !u.IsDeleted).FirstOrDefaultAsync();
            if (laying == null)
                return;

            laying.IsDeleted = true;

            await _dc.SaveChangesAsync();
        }

        public async Task<HbResult<HbLaying>> GetById(int id)
        {
            var laying = await _dc.Layings.Where(u => !u.IsDeleted)
                                          .Where(u => u.Id == id)
                                          .FirstOrDefaultAsync();

            if (laying == null)
                return new HbResult<HbLaying>(ErrorCodes.LayingNotFound);

            return new HbResult<HbLaying>(_mapper.Map<HbLaying>(laying));
        }

        public async Task<IEnumerable<HbLaying>> GetList(PagedLayingsForm form)
        {
            var query = _dc.Layings.Where(u => !u.IsDeleted)
                                   .Where(u => u.CreationTime > form.Start && u.CreationTime < form.End)
                                   .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId);
            if (form.IncubatorId.HasValue)
                query = query.Where(u => u.IncubatorId == form.IncubatorId);

            var layings = await query.OrderByDescending(u => u.Id)
                                     .Skip(form.Offset)
                                     .Take(form.Count)
                                     .ToArrayAsync();

            return layings.Select(_mapper.Map<HbLaying>).ToArray();
        }

        public async Task<HbResult<HbLaying>> Update(UpdateLayingForm form)
        {
            var laying = await _dc.Layings.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == form.Id);
            if (laying == null)
                return new HbResult<HbLaying>(ErrorCodes.LayingNotFound);

            var lotExist = await _dc.Lots.Where(u => !u.IsDeleted && u.Id == form.LotId).AnyAsync();
            if (!lotExist)
                return new HbResult<HbLaying>(ErrorCodes.LotNotFound);

            var incubatorExist = await _dc.Incubators.AnyAsync(u => !u.IsDeleted && u.Id == form.IncubatorId);
            if (!incubatorExist)
                return new HbResult<HbLaying>(ErrorCodes.IncubatorNotFound);

            var overheads = await _dc.Overheads
                                     .Where(u => !u.IsDeleted && u.LotId == form.LotId)
                                     .SumAsync(u => u.Amount);

            var purchases = await _dc.Purchases
                                     .Where(u => !u.IsDeleted && u.LotId == form.LotId)
                                     .SumAsync(u => u.Amount);

            var layings = await _dc.Layings.Where(u => !u.IsDeleted)
                                           .Where(u => u.LotId == form.LotId && u.Id != laying.Id)
                                           .ToArrayAsync();

            var eggPrice = (overheads + purchases) / (form.Count + layings.Sum(u => u.Count));

            laying.Count = form.Count;
            laying.EggPrice = eggPrice;
            laying.IncubatorId = form.IncubatorId;
            laying.LotId = form.LotId;
            laying.CreationTime = form.CreationDate;

            foreach (var item in layings)
                item.EggPrice = eggPrice;

            await _dc.SaveChangesAsync();

            return new HbResult<HbLaying>(_mapper.Map<HbLaying>(laying));
        }
    }
}
