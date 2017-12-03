using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HomeBird.DataBase.EfCore.Context
{
    public class HomeBirdContextFactory : IDesignTimeDbContextFactory<HomeBirdContext>
    {
        public HomeBirdContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HomeBirdContext>();
            builder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=HomeBird;User ID=HomeBirdDbMigrator;Password=erwWEw2#");

            return new HomeBirdContext(builder.Options);
        }
    }
}
