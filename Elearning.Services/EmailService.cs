using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Elearning.Contracts.Services;
using Elearning.Contracts.Common;

namespace Taskaty.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

                using (var client = new SmtpClient(emailSettings.SmtpServer, emailSettings.SmtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(emailSettings.SenderEmail),
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(email);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }
    }

}
