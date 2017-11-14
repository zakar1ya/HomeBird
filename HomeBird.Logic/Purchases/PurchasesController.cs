using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.Logic;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using HomeBird.DataClasses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HomeBird.Logic.Purchases
{
    public class PurchasesController : Controller
    {
        private readonly IPurchasesUnit _purchase;
        private readonly IMapper _mapper;
        private readonly ILotsUnit _lots;

        public PurchasesController(IPurchasesUnit purchase, IMapper mapper, ILotsUnit lots)
        {
            _purchase = purchase;
            _mapper = mapper;
            _lots = lots;
        }

        public async Task<IActionResult> List(PagedPurchasesForm form)
        {
            var page = await _purchase.GetList(form);
            form.Total = await _purchase.Count(form);
            return View(new PagedViewModel<HbPurchase, PagedPurchasesForm>(page, form));
        }

        public async Task<IActionResult> Add()
        {
            var form = new CreatePurchaseForm();
            await InitLotsList(form);

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreatePurchaseForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _purchase.Create(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var purchase = await _purchase.GetById(id);
            if (!purchase.IsCorrect)
                return RedirectToAction(nameof(List));

            var form = _mapper.Map<UpdatePurchaseForm>(purchase.Result);
            await InitLotsList(form);

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePurchaseForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _purchase.UpdatePurchase(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _purchase.Delete(id);
            return RedirectToAction(nameof(List));
        }

        private async Task InitLotsList(CreatePurchaseForm form)
        {
            form.Lots = await _lots.GetList(new PagedLotsForm
            {
                Start = new DateTime(form.PurchaseDate.Year, 1, 1),
                End = new DateTime(form.PurchaseDate.Year + 1, 1, 1)
            });
        }

    }
}
