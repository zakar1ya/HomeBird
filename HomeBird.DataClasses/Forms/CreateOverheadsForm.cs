using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeBird.DataClasses.Forms
{
    public class CreateOverheadsForm
    {
        public CreateOverheadsForm()
        {
            OverheadDate = DateTime.UtcNow;
        }

        [Required(ErrorMessage = "Укажите дату")]
        [Display(Name = "Дата")]
        public DateTime OverheadDate { get; set; }

        [Required(ErrorMessage = "Укажите сумму")]
        [Display(Name = "Сумма")]
        [Range(1, int.MaxValue, ErrorMessage = "Сумма должна быть больше 0")]
        public decimal Amount { get; set; }

        [StringLength(255, ErrorMessage = "Коммендарий не должен быть длиннее 255 символов")]
        [Display(Name = "Примечание")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Укажите номер партии")]
        [Display(Name = "Номер партии")]
        public int LotId { get; set; }

        public IEnumerable<HbLot> Lots { get; set; }
    }
}
