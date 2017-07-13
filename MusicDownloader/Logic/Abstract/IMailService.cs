using Logic.Models;

namespace Logic.Abstract
{
    public interface IMailService
    {
        void SendContactMeMessage(ContactMeMessageDTO contactMeMessage);
    }
}
