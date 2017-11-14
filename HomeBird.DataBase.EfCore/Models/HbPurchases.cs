using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    public class HbPurchases
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(2048)]
        public string Address { get; set; }
        
        public int LotId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("LotId")]
        public HbLots Lot { get; set; }
    }
}
