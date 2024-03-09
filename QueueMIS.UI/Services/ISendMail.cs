
namespace QueueMIS.Services
{
    public interface ISendMail
    {
        public Task<bool> SendEmailAsync(string patientName, string serviceType, string toEmail,bool isNotifyEmail);
    }
}
