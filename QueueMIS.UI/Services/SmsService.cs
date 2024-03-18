using AfricasTalkingCS;
using NUnit.Framework;
using System;
using System.Collections;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace QueueMIS.Services
{
    public class SmsService : ISmsService
    {
        public static string apiKey = "8991395dfddc1bf611b3b1d3f96275baae13a2d353c1bb27f4ba5cd09aed47d9";
        public static string username = "sandbox";
        public string env = "sandbox";

        public void SendSms(string toPhoneNumber, string message)
        {
            try
            {
                var gateway = new AfricasTalkingGateway(username, apiKey, env);

                string from = null; //$from = "shortCode or senderId";

                int bulkSMSMode = 1; // This should always be 1 for bulk messages

                Hashtable options = new Hashtable();
                options["enqueue"] = 1;

                // Any gateway errors will be captured by our custom Exception class below,
                // so wrap the call in a try-catch block  
                var sms = gateway.SendMessage(toPhoneNumber, message);
                foreach (var res in sms["SMSMessageData"]["Recipients"])
                {
                    Console.WriteLine((string)res["number"] + ": ");
                    Console.WriteLine((string)res["status"] + ": ");
                    Console.WriteLine((string)res["messageId"] + ": ");
                    Console.WriteLine((string)res["cost"] + ": ");
                }

                //Console.ReadLine();
               

            }
            catch (AfricasTalkingGatewayException exception)
            {
                Console.WriteLine(exception);
            }

        }

        //  private static AfricasTalkingGateway africasTalking = new AfricasTalkingGateway(username, apikey);




    }
}
//
