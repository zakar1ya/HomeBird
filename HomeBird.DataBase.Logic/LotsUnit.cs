using AutoMapper;
using HomeBird.DataBase.Ef6.Context;
using HomeBird.DataBase.Ef6.Models;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
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

        public void Update()
        {

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
