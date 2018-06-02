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
    internal class PurchasesUnit : IPurchasesUnit
    {
        private HomeBirdContext _dc;
        private IMapper _mapper;

        public PurchasesUnit(HomeBirdContext dc, IMapper mapper)
        {
            _dc = dc;
            _mapper = mapper;
        }

        public async Task<int> Count(PagedPurchasesForm form)
        {
            var query = _dc.Purchases
                .Where(u => !u.IsDeleted)
                .Where(u => u.PurchaseDate > form.Start && u.PurchaseDate < form.End)
                .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId);

            return await query.CountAsync();
        }

        public async Task<HbResult<HbPurchase>> Create(CreatePurchaseForm form)
        {
            var lotExist = await _dc.Lots.AnyAsync(u => !u.IsDeleted && u.Id == form.LotId);
            if (!lotExist)
                return new HbResult<HbPurchase>(ErrorCodes.LotNotFound);

            var purchase = _dc.Purchases.Add(new HbPurchases
            {
                Address = form.Address,
                Amount = form.Amount,
                Count = form.Count,
                LotId = form.LotId,
                PurchaseDate = form.PurchaseDate
            });

            await _dc.SaveChangesAsync();

            return new HbResult<HbPurchase>(_mapper.Map<HbPurchase>(purchase.Entity));
        }

        public async Task<HbResult<HbPurchase>> UpdatePurchase(UpdatePurchaseForm form)
        {
            var purchase = await _dc.Purchases.FirstOrDefaultAsync(u => u.Id == form.Id && !u.IsDeleted);
            if (purchase == null)
                return new HbResult<HbPurchase>(ErrorCodes.PurchaseNotFound);

            var lotExist = await _dc.Lots.AnyAsync(u => !u.IsDeleted && u.Id == form.LotId);
            if (!lotExist)
                return new HbResult<HbPurchase>(ErrorCodes.LotNotFound);

            purchase.Address = form.Address;
            purchase.Amount = form.Amount;
            purchase.Count = form.Count;
            purchase.LotId = form.LotId;
            purchase.PurchaseDate = form.PurchaseDate;

            await _dc.SaveChangesAsync();

            return new HbResult<HbPurchase>(_mapper.Map<HbPurchase>(purchase));
        }

        public async Task Delete(int purchaseId)
        {
            var purchase = await _dc.Purchases.FirstOrDefaultAsync(u => !u.IsDeleted && u.Id == purchaseId);
            if (purchase == null)
                return;

            purchase.IsDeleted = true;

            await _dc.SaveChangesAsync();
        }

        public async Task<HbResult<HbPurchase>> GetById(int purchaseId)
        {
            var purchase = await _dc.Purchases.Include(u => u.Lot)
                                              .FirstOrDefaultAsync(u => u.Id == purchaseId && !u.IsDeleted);
            if (purchase == null)
                return new HbResult<HbPurchase>(ErrorCodes.PurchaseNotFound);

            return new HbResult<HbPurchase>(_mapper.Map<HbPurchase>(purchase));
        }

        public async Task<IEnumerable<HbPurchase>> GetList(PagedPurchasesForm form)
        {
            var query = _dc.Purchases.Include(u => u.Lot)
                                     .Where(u => !u.IsDeleted)
                                     .Where(u => u.PurchaseDate > form.Start && u.PurchaseDate < form.End)
                                     .AsQueryable();

            if (form.LotId.HasValue)
                query = query.Where(u => u.LotId == form.LotId);

            var purchases = await query.OrderByDescending(u => u.Id)
                                       .Skip(form.Offset)
                                       .Take(form.Count)
                                       .ToArrayAsync();

            return purchases.Select(_mapper.Map<HbPurchase>).ToArray();
        }
    }
}
