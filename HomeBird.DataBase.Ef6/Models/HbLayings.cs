using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Ef6.Models
{
    public class HbLayings
    {
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
