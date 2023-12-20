using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class UrlDto
    {
        [RegularExpression(@"^(https?|ftp):\/\/[^\s\/$.?#].[^\s]*$", ErrorMessage ="Url is invalid")]
        public string Url { get; set; }
    }
}
