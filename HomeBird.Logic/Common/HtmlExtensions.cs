using HomeBird.DataClasses.Forms;
using HomeBird.DataClasses.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace HomeBird.Logic.Common
{
    public static class HtmlExtensions
    {
        public static IHtmlContent Paging(this IHtmlHelper helper, PagingForm form, Func<int, string> getPageFunc)
        {
            var vm = new PagingViewModel
            {
                Count = form.Count,
                Total = form.Total,
                GetUrl = getPageFunc,
                Offset = form.Offset
            };

            vm.Current = form.Offset / form.Count;
            vm.LastPage = form.Total / form.Count;

            vm.Start = vm.Current - 2 > 0 ? vm.Current - 2 : 1;
            vm.Stop = vm.Current + 2 < vm.LastPage ? vm.Current + 2 : vm.LastPage;

            return helper.Partial("~/Views/Shared/Paging.cshtml", vm);
        }

        public static string IsActive(this IHtmlHelper helper, string action, string controller = null)
        {
            if(!string.IsNullOrWhiteSpace(controller) && controller != helper.ViewContext.RouteData.Values["controller"].ToString())
                return string.Empty;

            if(action == helper.ViewContext.RouteData.Values["action"].ToString())
                return "active";

            return string.Empty;
        }
    }
}
