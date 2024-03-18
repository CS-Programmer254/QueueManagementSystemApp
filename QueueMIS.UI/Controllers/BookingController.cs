using Microsoft.AspNetCore.Mvc;
using QueueMIS.Services;
using QueueMIS.Settings;
using Microsoft.Extensions.Options;


namespace QueueMIS.Controllers
{
    public class BookingController(IBookingManager bookingManager, ISendMail sendMail, ISmsService sms) : Controller
    {
        private readonly IBookingManager _bookingManager = bookingManager;
        private readonly ISmsService _sms= sms;
        private readonly ISendMail _sendMail = sendMail;

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingManager.GetPatientBookingsInQueueAsync();
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(string patientName, string serviceType,string toEmail)
        {
            var bookingTime = DateTime.Now;
            await _bookingManager.AddPatientBookingAsync(patientName, bookingTime, serviceType,toEmail);


            TempData["success"] = "Booking Added successfully";

            // Send the email
            //await _sendMail.SendEmailAsync(patientName, serviceType,toEmail,false);
            // _sms.SendSms("+254769860886","Hi Mr Anderson!");
            //if (sent)
            //{
            //    TempData["success"] = "Email with booking details sent";
            //}
            SmsAPI.SendSms();
            TempData["success"] = "Email with booking details sent";

            return RedirectToAction("Index");
        }
       
        
    }
}
