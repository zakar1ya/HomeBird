using HomeBird.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeBird.DataClasses.Forms
{
    public class CreateSaleForm
    {
        public CreateSaleForm()
        {
            SaleDate = DateTime.UtcNow;
        }

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
        public SalesTypes Type { get; set; }

        [Display(Name = "Примечание")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Укажите партию")]
        [Display(Name = "Номер партии")]
        public int LotId { get; set; }

        public IEnumerable<HbLot> Lots { get; set; }
    }
}
