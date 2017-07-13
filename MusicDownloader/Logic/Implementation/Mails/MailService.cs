using System.Net.Mail;
using Logic.Abstract;
using Logic.Models;

namespace Logic.Implementation.Mails
{
    public class MailService : IMailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderEmail;
        private readonly string _receiverEmail;

        public MailService(SmtpClient smtpClient, string senderEmail, string receiverEmail)
        {
            _smtpClient = smtpClient;
            _senderEmail = senderEmail;
            _receiverEmail = receiverEmail;
        }
        
        public void SendContactMeMessage(ContactMeMessageDTO contactMeMessage)
        {
            MailMessage mailMessage = new MailMessage(_senderEmail, _receiverEmail, "Contact me",
                $"Name: {contactMeMessage.Name}; Email: {contactMeMessage.Email}; \n {contactMeMessage.Message}");

            _smtpClient.Send(mailMessage);
        }
    }
}
