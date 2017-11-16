using HomeBird.DataBase.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBird.DataBase.EfCore.Context
{
    public class HomeBirdContext : DbContext
    {
        public HomeBirdContext(DbContextOptions<HomeBirdContext> opts) : base(opts)
        {

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
