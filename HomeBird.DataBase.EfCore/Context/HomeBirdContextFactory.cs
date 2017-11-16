using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace HomeBird.DataBase.EfCore.Context
{
    public class HomeBirdContextFactory : IDesignTimeDbContextFactory<HomeBirdContext>
    {
        public HomeBirdContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HomeBirdContext>();
            builder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=HomeBirds;User ID=dbMigratorHb;Password=passTest!#");

            return new HomeBirdContext(builder.Options);
        }
    }
}
