using UrlShortener.Brokers.Storages;
using UrlShortener.Models;

namespace UrlShortener.Services.Foundations.Urls
{
    public class UrlService : IUrlService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILogger<UrlService> logger;

        public UrlService(IStorageBroker storageBroker,ILogger<UrlService> logger)
        {
            this.storageBroker = storageBroker;
            this.logger = logger;
        }

        public async ValueTask<UrlDto> AddUrlAsync(UrlDto urlDto, HttpContext context)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<Url> RetrieveAllUrls() =>
            this.storageBroker.SelectAllUrls();

        public async ValueTask<Url> RetrieveUrlByName(string shortUrl)
        {
            var url = RetrieveAllUrls().FirstOrDefault(u =>
                u.ShortUrl == shortUrl);

            return url;
        }
    }
}
