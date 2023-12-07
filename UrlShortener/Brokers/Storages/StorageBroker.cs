using EFxceptions;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Brokers.Storages
{
    public partial class StorageBroker : EFxceptionsContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source = wwwroot\\Data\\Urls.db";

            optionsBuilder.UseSqlite(connectionString);
        }

        public override void Dispose() { }
    }
}
