using HomeBird.DataBase.Ef6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Ef6.Context
{
    public class HomeBirdContext : DbContext
    {
        public HomeBirdContext(string connectionString) : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<HbLots> Lots { get; set; }

        public DbSet<HbBroods> Broods { get; set; }

        public DbSet<HbIncubators> Incubators { get; set; }

        public DbSet<HbLayings> Layings { get; set; }

        public DbSet<HbOverheads> Overheads { get; set; }

        public DbSet<HbPurchases> Purchases { get; set; }

        public DbSet<HbSales> Sales { get; set; }        
    }
}
