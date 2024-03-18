namespace QueueMIS.Services
{
    public interface ISmsService
    {
        void SendSms(string toPhoneNumber, string message);
    }
}
