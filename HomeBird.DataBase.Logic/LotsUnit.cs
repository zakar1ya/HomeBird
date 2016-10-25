using AutoMapper;
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
    public class LotsUnit
    {
        private HomeBirdContext _dc;
        private IMapper _mapper;

        public LotsUnit(HomeBirdContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task<HbLot> Create(CreateLotForm form)
        {
            // TODO: check if lot with same identifier already exist in this year

            var dbLot = _dc.Lots.Add(new HbLots
            {
                IdentifierNumber = form.IdentifierNumber,
                CreationDate = DateTime.UtcNow
            });

            await _dc.SaveChangesAsync();

            return _mapper.Map<HbLot>(dbLot);
        }

        public async Task<HbLot> Update(UpdateLotForm form)
        {
            var lot = await _dc.Lots.FirstOrDefaultAsync(u => u.Id == form.Id && !u.IsDeleted);
            if (lot == null)
                return null;

            lot.IdentifierNumber = form.IdentifierNumber;

            await _dc.SaveChangesAsync();

            return _mapper.Map<HbLot>(lot);
        }

        public async Task Delete(int lotId)
        {
            var lot = await _dc.Lots.FirstOrDefaultAsync(u => u.Id == lotId && !u.IsDeleted);
            if (lot == null)
                return;

            lot.IsDeleted = true;

            await _dc.SaveChangesAsync();
        }

        public async Task<IEnumerable<HbLot>> GetAll(PagedLotsForm form)
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

        public void GetById()
        {

        }
    }
}
