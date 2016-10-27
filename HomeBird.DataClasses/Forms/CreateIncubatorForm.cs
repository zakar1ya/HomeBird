using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses.Forms
{
    public class CreateIncubatorForm
    {
        [Required(ErrorMessage = "Укажите название инкубатора")]
        [StringLength(255, ErrorMessage = "Название должно быть меньше 255 символов")]
        [Display(Name = "Название")]
        public string Title { get; set; }
    }
}
