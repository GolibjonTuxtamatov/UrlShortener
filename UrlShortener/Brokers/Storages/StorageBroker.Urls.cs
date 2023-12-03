using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UrlShortener.Models;

namespace UrlShortener.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Url> Urls { get; set; }


        public async ValueTask<Url> InsertUrlAsync(Url url)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Url> storedUrl = await broker.Urls.AddAsync(url);
            await broker.SaveChangesAsync();

            return storedUrl.Entity;
        }

    }
}
