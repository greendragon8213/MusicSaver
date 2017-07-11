using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using MusicDownloader.Models;
using MusicDownloader.Resources;

namespace MusicDownloader.Controllers
{
    public class HomeController : BaseController
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

        public ActionResult About()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> SendContactMeMessage([FromBody]ContactMeMessageVM contactMeMessage)
        {
            //if (songsList == null || songsList.Count == 0)
            //    throw new ArgumentException(ErrorMessages.SongsListIsEmpty);

            //string temporaryFilesPath = Path.Combine(Server.MapPath("~"),
            //    (ConfigurationManager.AppSettings["TemporaryFilesFolderName"]));
            //string savedFileName = await _musicArchiveService.CreateMusicArchive(songsList, temporaryFilesPath);

            //return Json(new { fileName = savedFileName });
            return View();
        }
    }
}