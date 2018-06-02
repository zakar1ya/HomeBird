using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses
{
    public class HbOverhead
    {
        public int Id { get; set; }

        public DateTime OverheadDate { get; set; }

        public decimal Amount { get; set; }

        public string Comment { get; set; }

        public HbLot Lot { get; set; }

    }

    public class PagedOverheadForm : PagingForm
    {
        public PagedOverheadForm()
        {
            Start = new DateTime(DateTime.Now.Year, 1, 1);
            End = Start.Value.AddYears(1);
        }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public int? LotId { get; set; }
    }
}
