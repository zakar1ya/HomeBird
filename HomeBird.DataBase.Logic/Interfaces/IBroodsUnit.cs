using System.Collections.Generic;
using System.Threading.Tasks;
using HomeBird.Common;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;

namespace HomeBird.DataBase.Logic
{
    public interface IBroodsUnit
    {
        Task<HbResult<HbBrood>> Create(CreateBroodForm form);

        Task Remove(int id);

        Task<HbResult<HbBrood>> GetById(int id);

        Task<IEnumerable<HbBrood>> GetList(PagedBroodsForm form);

        Task<HbResult<HbBrood>> Update(UpdateBroodForm form);

        Task<int> Count(PagedBroodsForm form);
    }
}