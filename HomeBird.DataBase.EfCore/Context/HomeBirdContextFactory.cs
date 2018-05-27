using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HomeBird.DataBase.EfCore.Context
{
    public class HomeBirdContextFactory : IDesignTimeDbContextFactory<HomeBirdContext>
    {
        public HomeBirdContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HomeBirdContext>();
            builder.UseSqlServer("Data Source=78.47.113.139, 1433\\SQLEXPRESS;Initial Catalog=HomeBirdDev;User ID=HbDbMigrator;Password=odfj!2*e");

            return new HomeBirdContext(builder.Options);
        }
    }
}
