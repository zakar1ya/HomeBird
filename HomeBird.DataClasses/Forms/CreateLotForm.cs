using System;
using System.ComponentModel.DataAnnotations;

namespace HomeBird.DataClasses.Forms
{
    public class CreateLotForm
    {
        [Required(ErrorMessage = "Укажите номер партии")]
        [StringLength(15, ErrorMessage = "Идентификатор не должен быть менее 15 символов")]
        [Display(Name = "Номер партии")]
        public string IdentifierNumber { get; set; }

        public int Year { get; set; } = DateTimeOffset.UtcNow.Year;
    }
}
