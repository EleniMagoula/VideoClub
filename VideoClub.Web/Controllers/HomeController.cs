using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoClub.Infrastructure.Services;

namespace VideoClub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggingService _logger;

        public HomeController(ILoggingService logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            //_logger.Writer.Information("Hello, Serilog!");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}