using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBird.DataBase.EfCore.Models
{
    public class HbLayings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public int Count { get; set; }

        public decimal EggPrice { get; set; }

        public int LotId { get; set; }

        public int IncubatorId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("LotId")]
        public HbLots Lot { get; set; }
    }
}
