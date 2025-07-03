using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Utilities.Helpers
{
    public static class MailHelper
    {
        public static async Task SendEmailAsync(string smtpHost, int smtpPort, string smtpUser, string smtpPass, string toEmail, string subject, string body)
        {
            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(smtpUser, smtpPass);

                var mailMessage = new MailMessage(smtpUser, toEmail, subject, body);
                mailMessage.IsBodyHtml = true;

                await client.SendMailAsync(mailMessage);
            }
        }
    }
} 