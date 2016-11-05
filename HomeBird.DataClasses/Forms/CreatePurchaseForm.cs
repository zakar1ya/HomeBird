using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public class CreatePurchaseForm
    {
        [Required(ErrorMessage = "Укажите количество")]
        [Range(0, 1000000)]
        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Укажите сумму")]
        [Range(0, 1000000)]
        [Display(Name = "Сумма")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Укажите дату закупки")]
        [Display(Name = "Дата закупки")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Укажите адрес")]
        [StringLength(255, ErrorMessage = "Максимальная длина адреса 255 символов")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите партию")]
        [Display(Name = "Номер партии")]
        public int LotId { get; set; }

        public IEnumerable<SelectListItem> Lots { get; set; }
    }


}
