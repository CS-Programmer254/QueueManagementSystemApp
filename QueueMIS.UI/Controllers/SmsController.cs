using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Options;
using System.Configuration;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Core;
using QueueMIS.Settings;

namespace QueueMIS.Controllers
{
    public class SmsController : TwilioController
    {

        private readonly TwilioSettings _twilioSettings;
        public SmsController(IOptions<TwilioSettings> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
        }
        
        public ContentResult SendSms()
        {
            var message = MessageResource.Create(
                body: "Hello, this is a test message from Twilio!",
                from: new Twilio.Types.PhoneNumber(_twilioSettings.PhoneNumber),
                to: new Twilio.Types.PhoneNumber("+254769860886")
            );

            return Content(message.Sid);

            //ViewBag.MessageSid = message.Sid;

            //return View();
        }

    }
}
