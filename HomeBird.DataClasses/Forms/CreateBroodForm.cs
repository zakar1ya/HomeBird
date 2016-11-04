using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public class CreateBroodForm
    {
        [Required(ErrorMessage = "Укажите дату вывода")]
        [Display(Name = "Дата вывода")]
        public DateTime BroodDate { get; set; }

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Укажите количество без зародыша")]
        [Display(Name = "Количество без зародыша")]
        public int EmptyCount { get; set; }

        [Required(ErrorMessage = "Укажите количество без наклева")]
        [Display(Name = "Количество без наклева")]
        public int DeadCount { get; set; }

        [Required(ErrorMessage = "Укажите номер партии")]
        [Display(Name = "Номер партии")]
        public int LotId { get; set; }

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
