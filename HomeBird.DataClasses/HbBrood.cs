using System;

namespace HomeBird.DataClasses
{
    /// <summary>
    /// Выводок
    /// </summary>
    public class HbBrood
    {
        public int Id { get; set; }

        public DateTimeOffset BroodDate { get; set; }

        public int Count { get; set; }

        public decimal Percent { get; set; }

        public int EmptyCount { get; set; }

        public decimal EmptyPercent { get; set; }

        public int DeadCount { get; set; }

        public decimal DeadPercent { get; set; }

        public decimal PlacePrice { get; set; }

    }
}
