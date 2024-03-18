using Infobip.Api.Client;
using Infobip.Api.Client.Api;
using Infobip.Api.Client.Model;

namespace QueueMIS.Services
{
    public static class SmsAPI
    {
       public static void SendSms()
        {
            var configuration = new Configuration()
            {
                BasePath = "k25wg8.api.infobip.com",
                ApiKeyPrefix = "App",
                ApiKey = "380a55998a01730565f8fb2e7ad45158-38640ed5-9354-429b-9960-5762a6d3953b"
            };
           var sendSmsApi = new SendSmsApi(configuration);
            //var sendSmsApi = new SendSmsApi(myHttpClientInstance, configuration);
            var smsMessage = new SmsTextualMessage()
            {
                From = "InfoSMS",
                Destinations = new List<SmsDestination>()
                {
            new SmsDestination(to: "41793026727")
                },
                Text = "This is a dummy SMS message sent using Infobip.Api.Client"
            };

            var smsRequest = new SmsAdvancedTextualRequest()
            {
                Messages = new List<SmsTextualMessage>() { smsMessage }
            };
            try
            {
                var smsResponse = sendSmsApi.SendSmsMessage(smsRequest);

                System.Diagnostics.Debug.WriteLine($"Status: {smsResponse.Messages.First().Status}");
            }
            catch (ApiException apiException)
            {
                var errorCode = apiException.ErrorCode;
                var errorHeaders = apiException.Headers;
                var errorContent = apiException.ErrorContent;
            }
        }
    }
}
