using HomeBird.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Ef6.Models
{
    public class HbSales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime SaleDate { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }

        public string Buyer { get; set; }

        public BirdTypes Type { get; set; }

        public bool IsDeleted { get; set; }

        public string Comment { get; set; }

        public int LotId { get; set; }

        public HbLots Lot { get; set; }
    }
}
