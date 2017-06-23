using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using log4net;
using MusicDownloader.Resources;

namespace MusicDownloader.Controllers
{
    [HandleError]
    public abstract class BaseController : Controller
    {
        protected static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected BaseController()
        {
            SetThreadCultureByRequestHeader();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Logger.Error(filterContext.Exception.Message, filterContext.Exception);

            filterContext.ExceptionHandled = true;

            if (filterContext.Exception is ArgumentException)
            {
                Response.StatusCode = 400;
                filterContext.Result = GetErrorJsonResult(filterContext.Exception.Message);
                return;
            }

            if (filterContext.Exception is OutOfMemoryException)
            {
                Response.StatusCode = 500;
                filterContext.Result = GetErrorJsonResult(ErrorMessages.TooMuchFilesPerRequest);
                return;
            }

            if (filterContext.Exception is FileNotFoundException || filterContext.Exception is DirectoryNotFoundException)
            {
                Response.StatusCode = 404;
                filterContext.Result = GetErrorJsonResult(ErrorMessages.FileNotFound);
                return;
            }

            Response.StatusCode = 500;
            filterContext.Result = GetErrorJsonResult(filterContext.Exception.Message);

            //base.OnActionExecuting(filterContext);
        }

        #region Private methods

        private JsonResult GetErrorJsonResult(string message)
        {
            return Json(new { errorMessage = message }, 
                JsonRequestBehavior.AllowGet);
        }

        private void SetThreadCultureByRequestHeader()
        {
            var userCulture = CultureInfo.InvariantCulture;
            try
            {
                
                var userLanguages = System.Web.HttpContext.Current.Request.UserLanguages;
                userCulture = CultureInfo.GetCultureInfo(userLanguages[0]);
            }
            catch (Exception)
            {
                // ignored
            }

            Thread.CurrentThread.CurrentUICulture = userCulture;
        }

        #endregion
    }
}