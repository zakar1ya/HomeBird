using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    /// <summary>
    /// Инкубаторы
    /// </summary>
    public class HbIncubators
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [MaxLength(1024)]
        public virtual string Title { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}
