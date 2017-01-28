using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.Logic;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using HomeBird.DataClasses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.Logic.Sales
{
    public class SalesController : Controller
    {
        private readonly ISalesUnit _sales;
        private readonly ILotsUnit _lots;
        private readonly IMapper _mapper;

        public SalesController(ISalesUnit sales, ILotsUnit lots, IMapper mapper)
        {
            _sales = sales;
            _lots = lots;
            _mapper = mapper;
        }

        public async Task<IActionResult> List(PagedSalesForm form)
        {
            var page = await _sales.GetList(form);
            form.Total = await _sales.Count(form);
            return View(new PagedViewModel<HbSale, PagedSalesForm>(page, form));
        }

        public async Task<IActionResult> Add()
        {
            var form = new CreateSaleForm();
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateSaleForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _sales.Create(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sale = await _sales.GetById(id);
            if (!sale.IsCorrect)
                return RedirectToAction(nameof(List));

            var form = _mapper.Map<UpdateSaleForm>(sale.Result);
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSaleForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _sales.Update(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _sales.Delete(id);
            return RedirectToAction(nameof(List));
        }

        private async Task InitLotsList(CreateSaleForm form)
        {
            form.Lots = await _lots.GetList(new PagedLotsForm
            {
                Start = new DateTime(form.SaleDate.Year, 1, 1),
                End = new DateTime(form.SaleDate.Year + 1, 1, 1)
            });
        }
    }
}
