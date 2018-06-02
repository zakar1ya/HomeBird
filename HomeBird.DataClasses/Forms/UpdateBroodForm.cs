using System.ComponentModel.DataAnnotations;

namespace HomeBird.DataClasses.Forms
{
    public class UpdateBroodForm : CreateBroodForm
    {
        [Required]
        public int Id { get; set; }
    }
}
