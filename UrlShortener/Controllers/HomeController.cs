using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Services.Foundations.Urls;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUrlService urlService;

        public HomeController(ILogger<HomeController> logger, IUrlService urlService)
        {
            logger = logger;
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

            if(url == null)
            {
                logger.LogInformation("Post url is nul");
            }
            return View("Index", url);
        }

        [Route("/{shortUrl}")]
        public IActionResult RedirectToLongUrl(string shortUrl)
        {
            Url url = this.urlService.RetrieveUrlByName(shortUrl).Result;
            if(url is null)
            {
                logger.LogInformation("Redirect org url not found");
            }
            return Redirect(url.OrginalUrl);
        }
    }
}
