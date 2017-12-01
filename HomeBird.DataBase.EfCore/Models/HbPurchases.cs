using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    /// <summary>
    /// Закупки
    /// </summary>
    public class HbPurchases
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual DateTimeOffset CreationDate { get; set; }

        public virtual DateTimeOffset PurchaseDate { get; set; }

        public virtual int Count { get; set; }

        public virtual decimal Amount { get; set; }

        [MaxLength(2048)]
        public virtual string Address { get; set; }
        
        public virtual int LotId { get; set; }

        public virtual bool IsDeleted { get; set; }

        [ForeignKey(nameof(LotId))]
        public virtual HbLots Lot { get; set; }
    }
}
