using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using QueueMIS.Settings;
namespace QueueMIS.Services
{
    public class SendMail:ISendMail
    {
        private readonly MailSettings _mailSettings;
        public SendMail(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(string patientName, string serviceType, string toEmail,bool isNotifyEmail)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Queue Booking";
                var builder = new BodyBuilder();
                var message = (!isNotifyEmail) ? $"Dear {patientName},\nYou are booked for {serviceType} at {DateTime.Now}" : $"Dear {patientName},\nYou are next in the {serviceType} queue\n\r Get ready to be served";
                builder.HtmlBody = message;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
