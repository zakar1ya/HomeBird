using HomeBird.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    /// <summary>
    /// Продажи
    /// </summary>
    public class HbSales
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual DateTimeOffset SaleDate { get; set; }

        public virtual int Count { get; set; }

        public virtual decimal Amount { get; set; }

        [MaxLength(2048)]
        public virtual string Buyer { get; set; }

        public virtual SalesTypes Type { get; set; }

        public virtual bool IsDeleted { get; set; }

        [MaxLength(2048)]
        public virtual string Comment { get; set; }

        public virtual int LotId { get; set; }

        [ForeignKey(nameof(LotId))]
        public virtual HbLots Lot { get; set; }
    }
}
