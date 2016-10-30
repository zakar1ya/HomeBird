using HomeBird.DataClasses.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataClasses
{
    public class HbLaying
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public int Count { get; set; }

        public int LotId { get; set; }

        public int IncubatorId { get; set; }

        public decimal EggPrice { get; set; }
    }

    public class PagedLayingsForm : PagingForm
    {
        public PagedLayingsForm()
        {
            Start = new DateTime(DateTime.Now.Year, 1, 1);
            End = Start.Value.AddYears(1);
        }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public int? LotId { get; set; }

        public int? IncubatorId { get; set; }
    }
}
