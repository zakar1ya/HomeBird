using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    /// <summary>
    /// Закладки
    /// </summary>
    public class HbLayings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual int Count { get; set; }

        public virtual decimal EggPrice { get; set; }

        public virtual int LotId { get; set; }

        public virtual int IncubatorId { get; set; }

        public virtual bool IsDeleted { get; set; }

        [ForeignKey(nameof(LotId))]
        public virtual HbLots Lot { get; set; }
    }
}
