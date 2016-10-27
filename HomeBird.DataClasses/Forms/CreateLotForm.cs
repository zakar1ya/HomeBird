using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public class CreateLotForm
    {
        [Required(ErrorMessage = "Укажите номер партии")]
        [StringLength(15, ErrorMessage = "Идентификатор не должен быть менее 15 символов")]
        [Display(Name = "Номер партии")]
        public string IdentifierNumber { get; set; }
    }
}
