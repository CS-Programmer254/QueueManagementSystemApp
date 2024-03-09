using Microsoft.AspNetCore.Mvc;
using QueueMIS.Models;
using QueueMIS.Services;
using SendGrid.Helpers.Mail;

namespace QueueMIS.Controllers
{
    public class QueueController : Controller
    {
        private readonly QueueDbContext _queueDbContext;
        private readonly ISendMail _sendMail;

        public QueueController(QueueDbContext queueDbContext, ISendMail sendMail)
        {
            _queueDbContext= queueDbContext;
            _sendMail= sendMail;
        }

        public IActionResult Index()
        {
           // var patient = _queueDbContext.PatientBookings.FirstOrDefault(p =>p.IsServed=="Not Yet Served");
            var patient = _queueDbContext.PatientBookings.ToList();
            return View(patient);
        }

        public async Task<IActionResult> NextAsync(int patientId)
        {
            var patient = _queueDbContext.PatientBookings.Where(p => p.Id == patientId).FirstOrDefault();
            if (patient != null)
            {
                // Notify patient through email
                var isNotified = await _sendMail.SendEmailAsync(patient.PatientName,patient.ServiceType,patient.EmailAddress,true);
                if (isNotified)
                {
                    TempData["success"] = "Next Patient Has Been Notified Via Email";
                }
                // Update patient status
                patient.IsServed="Served";
                _queueDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }

}
