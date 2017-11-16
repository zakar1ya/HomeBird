using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    public class HbBroods
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime BroodDate { get; set; }

        public int Count { get; set; }

        public decimal Percent { get; set; }

        public int EmptyCount { get; set; }

        public decimal EmptyPercent { get; set; }

        public int DeadCount { get; set; }

        public decimal DeadPercent { get; set; }

        public decimal PlacePrice { get; set; }

        public int LotId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("LotId")]
        public virtual HbLots Lot { get; set; }
    }
}
