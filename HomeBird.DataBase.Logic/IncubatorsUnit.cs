using AutoMapper;
using HomeBird.DataBase.Ef6.Context;
using HomeBird.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Logic
{
    public class IncubatorsUnit
    {
        private readonly HomeBirdContext _dc;
        private readonly IMapper _mapper;

        public IncubatorsUnit(HomeBirdContext dc, IMapper mapper)
        {
            _mapper = mapper;
            _dc = dc;
        }

        public async Task<IEnumerable<HbIncubator>> GetList()
        {
            var incs = await _dc.Incubators
                                .Where(u => !u.IsDeleted)
                                .ToArrayAsync();

            return incs.Select(_mapper.Map<HbIncubator>).ToArray();
        }
    }
}
