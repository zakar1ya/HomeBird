using HomeBird.Common;
using HomeBird.DataBase.Ef6.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Logic
{
    public class PurchasesUnit
    {
        private HomeBirdContext _dc;

        public PurchasesUnit(HomeBirdContext dc)
        {
            _dc = dc;
        }

    }
}
