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

namespace HomeBird.DataBase.Logic
{
    internal class IncubatorsUnit : IIncubatorsUnit
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

        public async Task<HbIncubator> GetById(int incubatorId)
        {
            var inc = await _dc.Incubators.FirstOrDefaultAsync(u => u.Id == incubatorId && !u.IsDeleted);
            if (inc == null)
                return null;

            return _mapper.Map<HbIncubator>(inc);
        }

        public async Task<HbResult<HbIncubator>> Create(CreateIncubatorForm form)
        {
            var exist = await _dc.Incubators.AnyAsync(u => !u.IsDeleted && u.Title == form.Title);
            if (exist)
                return new HbResult<HbIncubator>(ErrorCodes.IncubatorAlreadyExist);

            var inc = _dc.Incubators.Add(new HbIncubators
            {
                Title = form.Title
            });

            await _dc.SaveChangesAsync();

            return new HbResult<HbIncubator>(_mapper.Map<HbIncubator>(inc));
        }

        public async Task<HbResult<HbIncubator>> Update(UpdateIncubatorForm form)
        {
            var inc = await _dc.Incubators.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == form.Id);
            if (inc == null)
                return new HbResult<HbIncubator>(ErrorCodes.IncubatorNotFound);

            inc.Title = form.Title;

            await _dc.SaveChangesAsync();

            return new HbResult<HbIncubator>(_mapper.Map<HbIncubator>(inc));
        }

        public async Task Delete(int incubatorId)
        {
            var inc = await _dc.Incubators.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == incubatorId);
            if (inc == null)
                return;

            inc.IsDeleted = true;

            await _dc.SaveChangesAsync();
        }
    }
}
