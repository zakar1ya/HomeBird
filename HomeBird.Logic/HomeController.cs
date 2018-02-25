using HomeBird.Common;
using HomeBird.Frontend.Logic.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeBird.Frontend.Logic
{
    public class HomeController : HbBaseController
    {
        public async Task<IActionResult> UpdateYear(int year, string returnUrl)
        {
            var claims = new Claim[] { new Claim(HbClaimTypes.CurrentYear, year.ToString()) };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddYears(99),
                IsPersistent = true,
                AllowRefresh = false
            });

            return Redirect(returnUrl);
        }
    }
}
