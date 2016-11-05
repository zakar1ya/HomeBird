using System.Collections.Generic;
using System.Threading.Tasks;
using HomeBird.Common;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;

namespace HomeBird.DataBase.Logic
{
    public interface IPurchasesUnit
    {
        Task<HbResult<HbPurchase>> Create(CreatePurchaseForm form);
        Task Delete(int purchaseId);
        Task<HbResult<HbPurchase>> GetById(int purchaseId);
        Task<IEnumerable<HbPurchase>> GetList(PagedPurchasesForm form);
        Task<HbResult<HbPurchase>> UpdatePurchase(UpdatePurchaseForm form);
        Task<int> Count(PagedPurchasesForm form);
    }
}