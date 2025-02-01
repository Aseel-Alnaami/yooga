using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using yogago.Models;

namespace yogago.Services
{
    public class EmailService
    {
        private readonly Emails _emailSettings;

        public EmailService(IConfiguration configuration)
        {
            // Ensure proper binding of the Emails section to _emailSettings
            _emailSettings = configuration.GetSection("Emails").Get<Emails>();
            if (_emailSettings == null)
            {
                throw new Exception("Failed to load email settings from configuration.");
            }
        }

        // The rest of the EmailService code remains the same
        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", recipientEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body // Email body (HTML supported)
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
