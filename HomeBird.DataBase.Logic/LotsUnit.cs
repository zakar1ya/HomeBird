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
            var lot = await _dc.Lots.FirstOrDefaultAsync(u => u.Id == form.Id);
            if (lot == null)
                return null;

            lot.IdentifierNumber = form.IdentifierNumber;

            await _dc.SaveChangesAsync();

            return _mapper.Map<HbLot>(lot);
        }

        public void Delete()
        {

        }

        public void GetAll()
        {

        }

        public void GetById()
        {

        }
    }
}
