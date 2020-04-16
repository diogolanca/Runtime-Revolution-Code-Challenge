using CodeChallenge.Entities;
using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodeChallenge.Controllers
{
    public class UrlController : Controller
    {
        private IManageUrl manageUrl;

        public UrlController(IManageUrl _manageUrl)
        {
            this.manageUrl = _manageUrl;
        }

        [HttpGet]
        public ActionResult Index()
         {
            Url url = new Url();
            return View(url);
        }

        public async Task<ActionResult> Index(Url url)
        {
            if (ModelState.IsValid)
            {
                ShortUrl shortUrl = await this.manageUrl.ShortenUrl(url.FullUrl, Request.UserHostAddress, url.CustomSegment);
                url.ShortUrl = string.Format("{0}://{1}{2}{3}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"), shortUrl.Segment);
            }
            return View(url);
        }

        public async Task<ActionResult> Click(string segment)
        {
            string referer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : string.Empty;
            Stat stat = await this.manageUrl.Click(segment, referer, Request.UserHostAddress);
            return this.RedirectPermanent(stat.ShortUrl.FullUrl);
        }
    }
}