using System;
using System.IO;
using System.Web.Mvc;
using log4net;

namespace MusicDownloader.Controllers
{
    [HandleError]
    public abstract class BaseController : Controller
    {
        protected static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                filterContext.Result = GetErrorJsonResult("You are trying to get too much files per request. Please try to get less.");
                return;
            }

            if (filterContext.Exception is FileNotFoundException || filterContext.Exception is DirectoryNotFoundException)
            {
                Response.StatusCode = 404;
                filterContext.Result = GetErrorJsonResult("The file you are looking for could not be found.");
                return;
            }

            Response.StatusCode = 500;
            filterContext.Result = GetErrorJsonResult(filterContext.Exception.Message);

            //base.OnActionExecuting(filterContext);
        }

        private JsonResult GetErrorJsonResult(string message)
        {
            return Json(new { errorMessage = message }, 
                JsonRequestBehavior.AllowGet);
        }
    }
}