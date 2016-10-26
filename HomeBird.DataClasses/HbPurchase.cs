using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses
{
    public class HbPurchase
    {
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }

        public string Address { get; set; }

        public int LotId { get; set; }
    }

    public class PagedPurchasesForm : PagingForm
    {
        public PagedPurchasesForm()
        {
            Start = new DateTime(DateTime.Now.Year, 1, 1);
            End = Start.Value.AddYears(1);
        }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public int? LotId { get; set; }
    }
}
