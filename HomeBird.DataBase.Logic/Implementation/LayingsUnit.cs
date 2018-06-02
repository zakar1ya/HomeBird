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
    internal class LayingsUnit : ILayingsUnit
    {
        private readonly HomeBirdContext _dc;
        private readonly IMapper _mapper;
        private readonly ILotsUnit _lots;

        public LayingsUnit(HomeBirdContext dc, IMapper mapper, ILotsUnit lots)
        {
            _dc = dc;
            _mapper = mapper;
            _lots = lots;
        }

        public async Task<int> Count(PagedLayingsForm form)
        {
            var query = _dc.Layings.Where(u => !u.IsDeleted)
                       .Where(u => u.LayingDate > form.Start && u.LayingDate < form.End)
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

            var laying = _dc.Layings.Add(new HbLayings
            {
                Count = form.Count,
                LayingDate = form.LayingDate,
                LotId = form.LotId,
                IncubatorId = form.IncubatorId,
                CreationDate = DateTimeOffset.UtcNow
            });

            await _dc.SaveChangesAsync();

            await _lots.RecalculateLot(form.LotId);

            return new HbResult<HbLaying>(_mapper.Map<HbLaying>(laying.Entity));
        }

        public async Task Delete(int id)
        {
            var laying = await _dc.Layings.Where(u => u.Id == id && !u.IsDeleted).FirstOrDefaultAsync();
            if (laying == null)
                return;

            laying.IsDeleted = true;

            await _dc.SaveChangesAsync();

            await _lots.RecalculateLot(laying.LotId);
        }

        public async Task<HbResult<HbLaying>> GetById(int id)
        {
            var laying = await _dc.Layings.Include(u => u.Incubator)
                                          .Include(u => u.Lot)
                                          .Where(u => !u.IsDeleted)
                                          .Where(u => u.Id == id)
                                          .FirstOrDefaultAsync();

            if (laying == null)
                return new HbResult<HbLaying>(ErrorCodes.LayingNotFound);

            return new HbResult<HbLaying>(_mapper.Map<HbLaying>(laying));
        }

        public async Task<IEnumerable<HbLaying>> GetList(PagedLayingsForm form)
        {
            var query = _dc.Layings.Include(u => u.Lot)
                                   .Include(u => u.Incubator)
                                   .Where(u => !u.IsDeleted)
                                   .Where(u => u.LayingDate > form.Start && u.LayingDate < form.End)
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
            
            laying.Count = form.Count;
            laying.IncubatorId = form.IncubatorId;
            laying.LotId = form.LotId;
            laying.LayingDate = form.LayingDate;           

            await _dc.SaveChangesAsync();

            await _lots.RecalculateLot(laying.LotId);

            return new HbResult<HbLaying>(_mapper.Map<HbLaying>(laying));
        }
    }
}
