using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public class CreateBroodForm : IValidatableObject
    {
        public CreateBroodForm()
        {
            BroodDate = DateTime.UtcNow;
        }

        [Required(ErrorMessage = "Укажите дату вывода")]
        [Display(Name = "Дата вывода")]
        public DateTime BroodDate { get; set; }

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество")]
        [Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Укажите количество без зародыша")]
        [Display(Name = "Количество без зародыша")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        public int EmptyCount { get; set; }

        [Required(ErrorMessage = "Укажите количество без наклева")]
        [Display(Name = "Количество без наклева")]
        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть больше 0")]
        public int DeadCount { get; set; }

        [Required(ErrorMessage = "Укажите номер партии")]
        [Display(Name = "Номер партии")]
        public int LotId { get; set; }
        public IEnumerable<HbLot> Lots { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (EmptyCount + DeadCount > Count)
                errors.Add(new ValidationResult("Сумма без наклева и без зародыша не долэна превышать общее количество."));

            return errors;
        }
    }

    public class PagedBroodsForm : PagingForm
    {
        public PagedBroodsForm()
        {
            Start = new DateTime(DateTime.UtcNow.Year, 1, 1);
            End = Start.Value.AddYears(1);
        }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public int? LotId { get; set; }

    }
}
