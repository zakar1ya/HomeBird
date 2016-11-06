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

namespace HomeBird.Logic.Broods
{
    public class BroodsController : Controller
    {
        private readonly IBroodsUnit _broods;
        private readonly ILotsUnit _lots;
        private readonly IMapper _mapper;

        public BroodsController(IBroodsUnit broods, ILotsUnit lots, IMapper mapper)
        {
            _broods = broods;
            _lots = lots;
            _mapper = mapper;
        }

        public async Task<IActionResult> List(PagedBroodsForm form)
        {
            var page = await _broods.GetList(form);
            form.Total = await _broods.Count(form);
            return View(new PagedViewModel<HbBrood, PagedBroodsForm>(page, form));
        }

        public async Task<IActionResult> Add()
        {
            var form = new CreateBroodForm();
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateBroodForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _broods.Create(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brood = await _broods.GetById(id);
            if (!brood.IsCorrect)
                return RedirectToAction(nameof(List));

            var form = _mapper.Map<UpdateBroodForm>(brood.Result);
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBroodForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _broods.Update(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.Result;
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _broods.Delete(id);
            return RedirectToAction(nameof(List));
        }

        private async Task InitLotsList(CreateBroodForm form)
        {
            var lots = await _lots.GetList(new PagedLotsForm
            {
                Start = new DateTime(form.BroodDate.Year, 1, 1),
                End = new DateTime(form.BroodDate.Year + 1, 1, 1)
            });

            form.Lots = lots.Select(u => new SelectListItem
            {
                Text = $"{u.IdentifierNumber} ({u.CreationDate})",
                Value = u.Id.ToString()
            }).ToArray();
        }

    }
}
