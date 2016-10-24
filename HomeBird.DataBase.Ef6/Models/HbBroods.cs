using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Ef6.Models
{
    public class HbBroods
    {
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

        [ForeignKey("LotId")]
        public virtual HbLots Lot { get; set; }
    }
}
