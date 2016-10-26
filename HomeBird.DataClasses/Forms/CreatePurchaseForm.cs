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
        public int Count { get; set; }

        [Required(ErrorMessage = "Укажите сумму")]
        [Range(0, 1000000)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Укажите дату закупки")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Укажите адрес")]
        [StringLength(255, ErrorMessage = "Максимальная длина адреса 255 символов")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите партию")]
        public int LotId { get; set; }
    }


}
