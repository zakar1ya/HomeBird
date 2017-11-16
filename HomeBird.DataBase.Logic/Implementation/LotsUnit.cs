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
    public class LotsUnit : ILotsUnit
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
            // TODO: check if lot with same identifier already exist in this year

            var dbLot = _dc.Lots.Add(new HbLots
            {
                IdentifierNumber = form.IdentifierNumber,
                CreationDate = DateTime.UtcNow
            });

            await _dc.SaveChangesAsync();

            return new HbResult<HbLot>(_mapper.Map<HbLot>(dbLot));
        }

        public async Task<HbResult<HbLot>> Update(UpdateLotForm form)
        {
            var lot = await _dc.Lots.FirstOrDefaultAsync(u => u.Id == form.Id && !u.IsDeleted);
            if (lot == null)
                return new HbResult<HbLot>(ErrorCodes.LotNotFound);

            lot.IdentifierNumber = form.IdentifierNumber;

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
                                .Where(u => u.CreationDate > form.Start && u.CreationDate < form.End)
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
    }
}
