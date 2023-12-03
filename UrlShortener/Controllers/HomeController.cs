using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Services.Foundations.Urls;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUrlService urlService;

        public HomeController(ILogger<HomeController> logger, IUrlService urlService = null)
        {
            _logger = logger;
            this.urlService = urlService;
        }

        public IActionResult Index(UrlDto urlDto)
        {
            return View(urlDto);
        }

        [HttpPost]
        public IActionResult PostUrl(UrlDto urlDto)
        {
            UrlDto url = this.urlService.AddUrlAsync(urlDto, HttpContext).Result;

            return View("Index", url);
        }

        [Route("/{shortUrl}")]
        public IActionResult RedirectToLongUrl(string shortUrl)
        {
            Url url = this.urlService.RetrieveUrlByName(shortUrl).Result;

            return Redirect(url.OrginalUrl);
        }
    }
}
