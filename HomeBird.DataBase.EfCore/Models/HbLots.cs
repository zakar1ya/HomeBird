using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    /// <summary>
    /// Партии
    /// </summary>
    public class HbLots
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [MaxLength(1024)]
        public virtual string Identifier { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual decimal? AvgAdultPrice { get; set; }

        public virtual decimal? AvgDailyPrice { get; set; }

        public virtual int? SoldCount { get; set; }

        public virtual int? Loses { get; set; }

        public virtual decimal? Profit { get; set; }

        public virtual decimal? EggPrice { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual int Year { get; set; }

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
