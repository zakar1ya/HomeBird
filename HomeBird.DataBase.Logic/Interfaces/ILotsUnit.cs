﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HomeBird.Common;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;

namespace HomeBird.DataBase.Logic
{
    public interface ILotsUnit
    {
        Task<HbResult<HbLot>> Create(CreateLotForm form);
        Task Delete(int lotId);
        Task<HbResult<HbLot>> GetById(int lotId);
        Task<IEnumerable<HbLot>> GetList(PagedLotsForm form);
        Task<HbResult<HbLot>> Update(UpdateLotForm form);
        Task<int> Count(PagedLotsForm form);
        Task RecalculateLot(int id);
    }
}