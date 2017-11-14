using HomeBird.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    public class HbSales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime SaleDate { get; set; }

        public int Count { get; set; }

        public decimal Amount { get; set; }

        [MaxLength(2048)]
        public string Buyer { get; set; }

        public BirdTypes Type { get; set; }

        public bool IsDeleted { get; set; }

        [MaxLength(2048)]
        public string Comment { get; set; }

        public int LotId { get; set; }

        public HbLots Lot { get; set; }
    }
}
