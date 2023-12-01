namespace UrlShortener.Models
{
    public class Url
    {
        public Guid Id { get; set; }
        public string OrginalUrl { get; set; }
        public string ShortUrl { get; set; }
    }
}
