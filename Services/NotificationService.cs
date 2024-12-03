using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ChocolateFactory.Services
{
    public class NotificationService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _emailFrom;
        private readonly string _emailPassword;

        public NotificationService(string smtpServer, int smtpPort, string emailFrom, string emailPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _emailFrom = emailFrom;
            _emailPassword = emailPassword;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_emailFrom, _emailPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailFrom),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
