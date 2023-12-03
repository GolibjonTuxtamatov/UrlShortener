using UrlShortener.Models;

namespace UrlShortener.Services.Foundations.Urls
{
    public interface IUrlService
    {
        ValueTask<UrlDto> AddUrlAsync(UrlDto urlDto,HttpContext context);
        IQueryable<Url> RetrieveAllUrls();

        ValueTask<Url> RetrieveUrlByName(string shortUrl);
    }
}
