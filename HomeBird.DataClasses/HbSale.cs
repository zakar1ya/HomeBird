using HomeBird.Common;
using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses
{
    public class HbSale
    {
        public int Id { get; set; }

        public DateTime SaleDate { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }

        public string Buyer { get; set; }

        public BirdTypes Type { get; set; }

        public string Comment { get; set; }

        public int LotId { get; set; }
    }

    public class PagedSalesForm : PagingForm
    {
        public PagedSalesForm()
        {
            Start = new DateTime(DateTime.Now.Year, 1, 1);
            End = Start.Value.AddYears(1);
        }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public BirdTypes? Type { get; set; }

        public int? LotId { get; set; }
    }
}
