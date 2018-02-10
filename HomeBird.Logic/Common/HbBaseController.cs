using HomeBird.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HomeBird.Frontend.Logic.Common
{
    public class HbBaseController : Controller
    {
        protected async Task<DateTimeOffset> CurrentStartDate()
        {
            var startDateClaim = User.Claims.FirstOrDefault(u => u.Type == HbClaimTypes.StartDate);

            if (startDateClaim == null)
            {
                var thisYear = new DateTimeOffset(DateTimeOffset.UtcNow.Year, 1, 1, 1, 0, 0, TimeSpan.Zero);
                var claims = new Claim[] { new Claim(HbClaimTypes.StartDate, thisYear.ToString()) };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddYears(99),
                    IsPersistent = true,
                    AllowRefresh = false
                });

                return thisYear;
            }

            return DateTimeOffset.Parse(startDateClaim.Value);
        }

        protected async Task<DateTimeOffset> CurrentEndDate()
        {
            var startEndDateClaim = User.Claims.FirstOrDefault(u => u.Type == HbClaimTypes.EndDate);

            if (startEndDateClaim == null)
            {
                var thisYear = new DateTimeOffset(DateTimeOffset.UtcNow.Year, 1, 1, 1, 0, 0, TimeSpan.Zero);
                var claims = new Claim[] { new Claim(HbClaimTypes.EndDate, thisYear.AddYears(1).ToString()) };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddYears(99),
                    IsPersistent = true,
                    AllowRefresh = false
                });

                return thisYear;
            }

            return DateTimeOffset.Parse(startEndDateClaim.Value);
        }
    }
}
