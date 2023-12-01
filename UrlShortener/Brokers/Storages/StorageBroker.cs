using EFxceptions;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
