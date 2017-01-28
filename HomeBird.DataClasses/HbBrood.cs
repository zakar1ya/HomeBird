using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses
{
    public class HbBrood
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

        public HbLot Lot { get; set; }

    }
}
