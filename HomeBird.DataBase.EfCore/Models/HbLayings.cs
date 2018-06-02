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

        public virtual DateTimeOffset LayingDate { get; set; }

        public virtual DateTimeOffset CreationDate { get; set; }

        public virtual int Count { get; set; }

        public virtual int LotId { get; set; }

        public virtual int IncubatorId { get; set; }

        public virtual bool IsDeleted { get; set; }

        [ForeignKey(nameof(LotId))]
        public virtual HbLots Lot { get; set; }

        [ForeignKey(nameof(IncubatorId))]
        public virtual HbIncubators Incubator { get; set; }
    }
}
