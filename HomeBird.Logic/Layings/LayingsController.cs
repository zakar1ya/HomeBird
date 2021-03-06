﻿using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.Logic;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using HomeBird.DataClasses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HomeBird.Logic.Layings
{
    public class LayingsController : Controller
    {
        private readonly ILayingsUnit _laying;
        private readonly ILotsUnit _lots;
        private readonly IMapper _mapper;
        private readonly IIncubatorsUnit _incub;

        public LayingsController(ILayingsUnit laying, ILotsUnit lots, IIncubatorsUnit incub, IMapper mapper)
        {
            _laying = laying;
            _lots = lots;
            _mapper = mapper;
            _incub = incub;
        }

        public async Task<IActionResult> List(PagedLayingsForm form)
        {
            var page = await _laying.GetList(form);
            form.Total = await _laying.Count(form);
            return View(new PagedViewModel<HbLaying, PagedLayingsForm>(page, form));
        }

        public async Task<IActionResult> Add()
        {
            var form = new CreateLayingForm();
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateLayingForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _laying.Create(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var laying = await _laying.GetById(id);
            if (!laying.IsCorrect)
                return RedirectToAction(nameof(List));

            var form = _mapper.Map<UpdateLayingForm>(laying.Result);
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateLayingForm form)
        {
            if (!ModelState.IsValid)
            {
                await InitLotsList(form);
                return View(form);
            }

            var res = await _laying.Update(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            await InitLotsList(form);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _laying.Delete(id);
            return RedirectToAction(nameof(List));
        }

        private async Task InitLotsList(CreateLayingForm form)
        {
            form.Lots = await _lots.GetList(new PagedLotsForm());

            form.Incubators = await _incub.GetList();
        }


    }
}
