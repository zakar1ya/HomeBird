using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeBird.DataClasses.Forms
{
    public class CreateLayingForm
    {
        [Required(ErrorMessage = "Укажиет дату закладки")]
        [Display(Name = "Дата закладки")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Укажите партию")]
        [Display(Name = "Партия")]
        public int LotId { get; set; }

        [Required(ErrorMessage = "Укажите инкубатор")]
        [Display(Name = "Инкубатор")]
        public int IncubatorId { get; set; }

        public IEnumerable<SelectListItem> Lots { get; set; }
    }
}
