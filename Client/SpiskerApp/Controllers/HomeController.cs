using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices.HostingModels;
using Microsoft.AspNetCore.SpaServices.Prerendering;
using Polly;
using SpiskerApp.Server.Helpers;

namespace SpiskerApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var prerenderResult = await Request.BuildPrerender();

            ViewData["SpaHtml"] = prerenderResult.Html; // our <app-root /> from Angular
            ViewData["Title"] = prerenderResult.Globals["title"]; // set our <title> from Angular
            ViewData["Styles"] = prerenderResult.Globals["styles"]; // put styles in the correct place
            ViewData["Scripts"] = prerenderResult.Globals["scripts"]; // scripts (that were in our header)
            ViewData["Meta"] = prerenderResult.Globals["meta"]; // set our <meta> SEO tags
            ViewData["Links"] = prerenderResult.Globals["links"]; // set our <link rel="canonical"> etc SEO tags
            ViewData["TransferData"] =
                prerenderResult.Globals["transferData"]; // our transfer data set to window.TRANSFER_CACHE = {};
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
