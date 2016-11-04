using System.Collections.Generic;
using System.Threading.Tasks;
using HomeBird.Common;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;

namespace HomeBird.DataBase.Logic
{
    public interface IIncubatorsUnit
    {
        Task<HbResult<HbIncubator>> Create(CreateIncubatorForm form);
        Task Delete(int incubatorId);
        Task<HbIncubator> GetById(int incubatorId);
        Task<IEnumerable<HbIncubator>> GetList();
        Task<HbResult<HbIncubator>> Update(UpdateIncubatorForm form);
    }
}