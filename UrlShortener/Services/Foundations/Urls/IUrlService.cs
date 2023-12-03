using UrlShortener.Models;

namespace UrlShortener.Services.Foundations.Urls
{
    public interface IUrlService
    {
        ValueTask<UrlDto> AddUrlsAsync(UrlDto urlDto,HttpContext context);
        IQueryable<Url> RerieveAllUrls();
    }
}
