using UrlShortener.Brokers.Storages;
using UrlShortener.Models;

namespace UrlShortener.Services.Foundations.Urls
{
    public class UrlService : IUrlService
    {
        private readonly IStorageBroker storageBroker;

        public UrlService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<UrlDto> AddUrlsAsync(UrlDto urlDto, HttpContext context)
        {
            if (!Uri.TryCreate(urlDto.Url, UriKind.Absolute, out var uri))
                Results.BadRequest("Invalid url");

            var random = new Random();
            const string chars =
                "QWERTYUIOPASDFGHJKLZXCVBNM1234567890@qwertyuiopasdfghjklzxcvbnm";

            string randomString =
                new string(Enumerable.Repeat(chars, 8)
                .Select(x => x[random.Next(x.Length)]).ToArray());

            var storedUrl = await this.storageBroker.InsertUrlAsync(
                new Url
                {
                    OrginalUrl = urlDto.Url,
                    ShortUrl = randomString,
                });

            string result = $"{context.Request.Scheme}://{context.Request.Host}/{storedUrl.ShortUrl}";

            urlDto.Url = result;

            return urlDto;
        }

        public IQueryable<Url> RerieveAllUrls() =>
            this.storageBroker.SelectAllUrls();
    }
}
