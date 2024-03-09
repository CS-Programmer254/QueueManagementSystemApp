using Microsoft.AspNetCore.Mvc;
using QueueMIS.Models;
using System.Diagnostics;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Options;
using QueueMIS.Settings;
namespace QueueMIS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger,IOptions<TwilioSettings> twilioSettings)
        {
            _logger = logger;
          
        }
        
        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
