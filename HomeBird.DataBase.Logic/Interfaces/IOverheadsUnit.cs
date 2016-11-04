using System.Collections.Generic;
using System.Threading.Tasks;
using HomeBird.Common;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;

namespace HomeBird.DataBase.Logic
{
    public interface IOverheadsUnit
    {
        Task<HbResult<HbOverhead>> Create(CreateOverheadsForm form);
        Task Delete(int overheadId);
        Task<IEnumerable<HbOverhead>> GetList(PagedOverheadForm form);
        Task<HbResult<HbOverhead>> Update(UpdateOverheadsForm form);
    }
}