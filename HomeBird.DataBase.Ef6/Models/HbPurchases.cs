using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Ef6.Models
{
    public class HbPurchases
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }

        public string Address { get; set; }
        
        public int LotId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("LotId")]
        public HbLots Lot { get; set; }
    }
}
