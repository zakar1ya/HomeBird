using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    /// <summary>
    /// Выводки
    /// </summary>
    public class HbBroods
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual DateTimeOffset CreationDate { get; set; }

        public virtual DateTimeOffset BroodDate { get; set; }

        public virtual int Count { get; set; }

        public virtual decimal Percent { get; set; }

        public virtual int EmptyCount { get; set; }

        public virtual decimal EmptyPercent { get; set; }

        public virtual int DeadCount { get; set; }

        public virtual decimal DeadPercent { get; set; }

        public virtual decimal PlacePrice { get; set; }

        public virtual int LotId { get; set; }

        public virtual bool IsDeleted { get; set; }

        [ForeignKey(nameof(LotId))]
        public virtual HbLots Lot { get; set; }
    }
}
