using HomeBird.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public class CreateSaleForm
    {
        [Required(ErrorMessage = "Укажите дату продажи")]
        [Display(Name = "Дата продажи")]
        public DateTime SaleDate { get; set; }

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Укажите сумму")]
        [Display(Name = "Сумма")]
        public decimal Amount { get; set; }

        [Display(Name = "Покупатель")]
        public string Buyer { get; set; }

        [Required(ErrorMessage = "Укажите возраст")]
        [Display(Name = "Тип (суточный/подрост)")]
        public BirdTypes Type { get; set; }

        [Display(Name = "Примечание")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Укажите партию")]
        [Display(Name = "Номер партии")]
        public int LotId { get; set; }

        public IEnumerable<SelectListItem> Lots { get; set; }
    }
}
