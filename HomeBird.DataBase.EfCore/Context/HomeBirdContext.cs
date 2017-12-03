using HomeBird.DataBase.EfCore.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBird.DataBase.EfCore.Context
{
    public class HomeBirdContext : DbContext
    {
        public HomeBirdContext(DbContextOptions<HomeBirdContext> opts) : base(opts)
        {

        }

        public virtual DbSet<HbLots> Lots { get; set; }

        public virtual DbSet<HbBroods> Broods { get; set; }

        public virtual DbSet<HbIncubators> Incubators { get; set; }

        public virtual DbSet<HbLayings> Layings { get; set; }

        public virtual DbSet<HbOverheads> Overheads { get; set; }

        public virtual DbSet<HbPurchases> Purchases { get; set; }

        public virtual DbSet<HbSales> Sales { get; set; }

    }
}
