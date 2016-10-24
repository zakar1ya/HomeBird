using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBird.DataBase.Ef6.Context
{
    public class HomeBirdContextFactory : IDbContextFactory<HomeBirdContext>
    {
        public HomeBirdContext Create()
        {
            return new HomeBirdContext("Data Source=192.168.1.232, 49170;Initial Catalog=HomeBirds;User ID=dbMigrator;Password=123123");
        }
    }
}
