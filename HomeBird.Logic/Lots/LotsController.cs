using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.Logic;
using HomeBird.DataClasses;
using HomeBird.DataClasses.Forms;
using HomeBird.DataClasses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeBird.Logic.Lots
{
    public class LotsController : Controller
    {
        private readonly ILotsUnit _lots;
        private readonly IMapper _mapper;

        public LotsController(ILotsUnit lots, IMapper mapper)
        {
            _lots = lots;
            _mapper = mapper;
        }

        public async Task<IActionResult> List(PagedLotsForm form)
        {
            var page = await _lots.GetList(form);
            form.Total = await _lots.Count(form);
            return View(new PagedViewModel<HbLot, PagedLotsForm>(page, form));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateLotForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var res = await _lots.Create(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            return View(form);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var lot = await _lots.GetById(id);
            if (!lot.IsCorrect)
                return RedirectToAction(nameof(List));

            var form = _mapper.Map<UpdateLotForm>(lot.Result);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateLotForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var res = await _lots.Update(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _lots.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
