using DAL.DAO;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class APIDbContext : DbContext
    {
        // send to a config file
        const string connectionString = "Server=tcp:tobolanos.database.windows.net,1433;Initial Catalog=tomy_HelloWorld;Persist Security Info=False;User ID=tomy.bolanos;Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public APIDbContext() : base() { }

        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
