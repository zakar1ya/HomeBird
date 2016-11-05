using AutoMapper;
using HomeBird.Common;
using HomeBird.DataBase.Logic;
using HomeBird.DataClasses.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeBird.Logic.Incubators
{
    public class IncubatorsController : Controller
    {
        private readonly IIncubatorsUnit _inc;
        public readonly IMapper _mapper;

        public IncubatorsController(IIncubatorsUnit inc, IMapper mapper)
        {
            _inc = inc;
            _mapper = mapper;
        }

        public async Task<IActionResult> List()
        {
            var incubators = await _inc.GetList();
            return View(incubators);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateIncubatorForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var res = await _inc.Create(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _inc.Delete(id);

            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var incubator = await _inc.GetById(id);
            if (incubator == null)
                return RedirectToAction(nameof(List));

            return View(_mapper.Map<UpdateIncubatorForm>(incubator));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateIncubatorForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var res = await _inc.Update(form);
            if (res.IsCorrect)
                return RedirectToAction(nameof(List));

            ViewData[ViewDataKeys.ErrorMessage] = res.ErrorMessage;
            return View(form);
        }
    }
}
