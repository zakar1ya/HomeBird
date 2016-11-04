using System.Collections.Generic;
using System.Threading.Tasks;
using HomeBird.Common;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;

namespace HomeBird.DataBase.Logic
{
    public interface ILayingsUnit
    {
        Task<HbResult<HbLaying>> Create(CreateLayingForm form);
        Task Delete(int id);
        Task<HbResult<HbLaying>> GetById(int id);
        Task<IEnumerable<HbLaying>> GetList(PagedLayingsForm form);
        Task<HbResult<HbLaying>> Update(UpdateLayingForm form);
    }
}