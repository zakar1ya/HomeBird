using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses
{
    public class HbLot
    {
        public int Id { get; set; }

        public string IdentifierNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal? AvgAdultPrice { get; set; }

        public decimal? AvgDailyPrice { get; set; }

        public int? SoldCount { get; set; }

        public int? Loses { get; set; }

        public decimal? Profit { get; set; }

    }

    public class PagedLotsForm : PagingForm
    {
        public PagedLotsForm()
        {
            Start = new DateTime(DateTime.UtcNow.Year, 1, 1);
            End = Start.AddYears(1);
        }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
