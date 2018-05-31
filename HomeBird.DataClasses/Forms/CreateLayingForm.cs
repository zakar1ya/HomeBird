using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeBird.DataClasses.Forms
{
    public class CreateLayingForm
    {
        public CreateLayingForm()
        {
            CreationDate = DateTime.UtcNow;
        }

        [Required(ErrorMessage = "Укажиет дату закладки")]
        [Display(Name = "Дата закладки")]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Укажите партию")]
        [Display(Name = "Партия")]
        public int LotId { get; set; }

        [Required(ErrorMessage = "Укажите инкубатор")]
        [Display(Name = "Инкубатор")]
        public int IncubatorId { get; set; }

        public IEnumerable<HbIncubator> Incubators { get; set; }
        public IEnumerable<HbLot> Lots { get; set; }
    }
}
