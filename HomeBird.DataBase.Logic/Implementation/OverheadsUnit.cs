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
    internal class OverheadsUnit : IOverheadsUnit
    {
        private HomeBirdContext _dc;
        private IMapper _mapper;

        public OverheadsUnit(HomeBirdContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task<int> Count(PagedOverheadForm form)
        {
            var query = _dc.Overheads
               .Where(u => !u.IsDeleted)
               .Where(u => u.OverheadDate > form.Start && u.OverheadDate < form.End)
               .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId.Value);

            return await query.CountAsync();
        }

        public async Task<HbResult<HbOverhead>> Create(CreateOverheadsForm form)
        {
            var lot = await _dc.Lots.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == form.LotId);
            if (lot == null)
                return new HbResult<HbOverhead>(ErrorCodes.LotNotFound);

            var res = _dc.Overheads.Add(new HbOverheads
            {
                Amount = form.Amount,
                Comment = form.Comment,
                LotId = form.LotId,
                OverheadDate = form.OverheadDate
            });

            await _dc.SaveChangesAsync();

            return new HbResult<HbOverhead>(_mapper.Map<HbOverhead>(res));
        }

        public async Task<HbResult<HbOverhead>> Update(UpdateOverheadsForm form)
        {
            var overhead = await _dc.Overheads.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == form.Id);
            if (overhead == null)
                return new HbResult<HbOverhead>(ErrorCodes.OverheadsNotFound);

            var lotExist = await _dc.Lots.AnyAsync(u => !u.IsDeleted && u.Id == form.LotId);
            if (!lotExist)
                return new HbResult<HbOverhead>(ErrorCodes.LotNotFound);

            overhead.Amount = form.Amount;
            overhead.Comment = form.Comment;
            overhead.LotId = form.LotId;
            overhead.OverheadDate = form.OverheadDate;

            await _dc.SaveChangesAsync();

            return new HbResult<HbOverhead>(_mapper.Map<HbOverhead>(overhead));
        }

        public async Task Delete(int overheadId)
        {
            var overhead = await _dc.Overheads.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == overheadId);
            if (overhead == null)
                return;

            overhead.IsDeleted = true;

            await _dc.SaveChangesAsync();
        }

        public async Task<IEnumerable<HbOverhead>> GetList(PagedOverheadForm form)
        {
            var query = _dc.Overheads.Include(u => u.Lot)
                                     .Where(u => !u.IsDeleted)
                                     .Where(u => u.OverheadDate > form.Start && u.OverheadDate < form.End)
                                     .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId.Value);

            var overheads = await query.OrderByDescending(u => u.OverheadDate)
                                       .Skip(form.Offset)
                                       .Take(form.Count)
                                       .ToArrayAsync();

            return overheads.Select(_mapper.Map<HbOverhead>).ToArray();
        }

        public async Task<HbResult<HbOverhead>> GetById(int id)
        {
            var overhead = await _dc.Overheads.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (overhead == null)
                return new HbResult<HbOverhead>(ErrorCodes.OverheadsNotFound);

            return new HbResult<HbOverhead>(_mapper.Map<HbOverhead>(overhead));
        }
    }
}
