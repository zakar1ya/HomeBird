using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    public class HbLots
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(1024)]
        public string IdentifierNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal? AvgAdultPrice { get; set; }

        public decimal? AvgDailyPrice { get; set; }

        public int? SoldCount { get; set; }

        public int? Loses { get; set; }

        public decimal? Profit { get; set; }

        public bool IsDeleted { get; set; }

        [InverseProperty("Lot")]
        public virtual ICollection<HbPurchases> Purchases { get; set; }

        [InverseProperty("Lot")]
        public virtual ICollection<HbOverheads> Overheads { get; set; }

        [InverseProperty("Lot")]
        public virtual ICollection<HbSales> Sales { get; set; }

        [InverseProperty("Lot")]
        public virtual ICollection<HbLayings> Layings { get; set; }

        [InverseProperty("Lot")]
        public virtual ICollection<HbBroods> Broods { get; set; }
    }
}
