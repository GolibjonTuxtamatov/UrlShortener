using UrlShortener.Models;

namespace UrlShortener.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Url> InsertUrlAsync(Url url);
        IQueryable<Url> SelectAllUrls();
    }
}
