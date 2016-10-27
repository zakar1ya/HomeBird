using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Ef6.Models
{
    public class HbOverheads
    {
        public int Id { get; set; }

        public DateTime OverheadDate { get; set; }

        public decimal Amount { get; set; }

        public string Comment { get; set; }

        public int LotId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("LotId")]
        public HbLots Lot { get; set; }
    }
}
