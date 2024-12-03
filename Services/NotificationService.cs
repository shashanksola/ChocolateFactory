using SendGrid;
using SendGrid.Helpers.Mail;

namespace ChocolateFactoryManagement.Services
{
    public class NotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var client = new SendGridClient(_configuration["SendGrid:ApiKey"]);
            var from = new EmailAddress(_configuration["SendGrid:FromEmail"], "Chocolate Factory");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            await client.SendEmailAsync(msg);
        }
    }
}
