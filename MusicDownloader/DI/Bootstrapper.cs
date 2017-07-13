using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Mvc;
using log4net;
using Logic.Abstract;
using Logic.Implementation;
using Logic.Implementation.Mails;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace DI
{
    public static class Bootstrapper
    {
        public static void Initialise(ILog logger)
        {
            IUnityContainer container = BuildUnityContainer(logger);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer(ILog logger)
        {
            var container = new UnityContainer();

            container.RegisterInstance<ILog>(logger);

            //log4net
            //container.RegisterInstance<ILog>(LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));


            container.RegisterType<ISongUrlProvider, ZfFmSongUrlProvider>(new PerRequestLifetimeManager());
            container.RegisterType<IMusicArchiveService, MusicArchiveService>(new PerRequestLifetimeManager());
            container.RegisterType<ISongDownloader, SongDownloader>(new PerRequestLifetimeManager());

            container.RegisterType<IMailService>(new InjectionFactory(_ => new MailService(
                new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(
                        WebConfigurationManager.AppSettings["emailService:SenderEmailAddress"],
                        WebConfigurationManager.AppSettings["emailService:SenderPassword"]),
                    EnableSsl = true,
                },
                WebConfigurationManager.AppSettings["emailService:SenderEmailAddress"],
                WebConfigurationManager.AppSettings["emailService:ReceiverEmailAddress"])));

            return container;
        }
    }
}