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

namespace HomeBird.Logic.Overheads
{
    public class OverheadsController : Controller
    {
        private readonly IOverheadsUnit _overheads;
        private readonly ILotsUnit _lots;
        private readonly IMapper _mapper;

        public OverheadsController(IOverheadsUnit overheads, ILotsUnit lots, IMapper mapper)
        {
            _overheads = overheads;
            _lots = lots;
            _mapper = mapper;
        }

        public async Task<IActionResult> List(PagedOverheadForm form)
        {
            var page = await _overheads.GetList(form);
            form.Total = await _overheads.Count(form);
            return View(new PagedViewModel<HbOverhead, PagedOverheadForm>(page, form));
        }

        public async Task<IActionResult> Add()
        {
            var form = new CreateOverheadsForm();
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateOverheadsForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _overheads.Create(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var overhead = await _overheads.GetById(id);
            if (!overhead.IsCorrect)
                return RedirectToAction(nameof(List));

            var form = _mapper.Map<UpdateOverheadsForm>(overhead.Result);
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateOverheadsForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _overheads.Update(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _overheads.Delete(id);
            return RedirectToAction(nameof(List));
        }

        private async Task InitLotsList(CreateOverheadsForm form)
        {
            var lots = await _lots.GetList(new PagedLotsForm
            {
                Start = new DateTime(form.OverheadDate.Year, 1, 1),
                End = new DateTime(form.OverheadDate.Year + 1, 1, 1)
            });

            form.Lots = lots.Select(u => new SelectListItem
            {
                Text = $"{u.IdentifierNumber} ({u.CreationDate})",
                Value = u.Id.ToString()
            }).ToArray();
        }
    }
}
