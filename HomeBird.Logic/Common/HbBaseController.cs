using HomeBird.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HomeBird.Frontend.Logic.Common
{
    public class HbBaseController : Controller
    {
        protected int CurrenYear
        {
            get
            {
                var yearClaim = User.Claims.FirstOrDefault(u => u.Type == HbClaimTypes.CurrentYear);

                return yearClaim == null ? DateTimeOffset.Now.Year : int.Parse(yearClaim.Value);
            }
        }        
    }
}
