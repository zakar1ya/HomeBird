using System.Collections.Generic;
using System.Threading.Tasks;
using HomeBird.Common;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;

namespace HomeBird.DataBase.Logic
{
    public interface ISalesUnit
    {
        Task<HbResult<HbSale>> Create(CreateSaleForm form);
        Task Delete(int id);
        Task<HbResult<HbSale>> GetById(int id);
        Task<IEnumerable<HbSale>> GetList(PagedSalesForm form);
        Task<HbResult<HbSale>> Update(UpdateSaleForm form);
        Task<int> Count(PagedSalesForm form);
    }
}