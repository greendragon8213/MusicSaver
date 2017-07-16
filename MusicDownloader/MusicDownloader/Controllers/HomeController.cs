using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using log4net;
using Logic.Abstract;
using Logic.Models;
using MusicDownloader.Models;

namespace MusicDownloader.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public HomeController(ILog logger, IMailService mailService):base(logger)
        {
            _mailService = mailService;
            _mapper = Mapper.Mapper.MapperInstance;
        }

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
            _mailService.SendContactMeMessage(_mapper.Map<ContactMeMessageVM, ContactMeMessageDTO>(contactMeMessage));
            
            return Json(new {});
        }
    }
}