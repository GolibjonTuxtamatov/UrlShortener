using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Url> Urls { get; set; }
    }
}
