using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ICSharpCode.SharpZipLib.Zip;
using Logic;
using Logic.Exceptions;
using Logic.Implementation;
using Logic.Models;

namespace MusicDownloader.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HowItWorks()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }
    }
}