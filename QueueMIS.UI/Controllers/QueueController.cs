using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueueMIS.Models;
using QueueMIS.Services;

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

        public IActionResult Index(string searchText)
        {
            var allPatients = _queueDbContext.PatientBookings.ToList();
            var servedPatients = allPatients.Where(p => p.IsServed == "Served").ToList();
            var notServedPatients = allPatients.Where(p => p.IsServed == "Not Yet Served").ToList();

            // Retrieve the next two patients
            var nextPatients = allPatients.Where(p => p.IsServed == "Not Yet Served").OrderBy(p => p.BookingTime).Take(2).ToList();
            var nextPatient = nextPatients.Count > 0 ? nextPatients[0] : null;
            var secondNextPatient = nextPatients.Count > 1 ? nextPatients[1] : null;

            if (!string.IsNullOrEmpty(searchText))
            {
                // Perform the search based on the provided search text
                servedPatients = servedPatients.Where(p =>
                    p.PatientName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.EmailAddress.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.ServiceType.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.BookingTime.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.IsServed.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase)
                ).ToList();

                notServedPatients = notServedPatients.Where(p =>
                    p.PatientName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.EmailAddress.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.ServiceType.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.BookingTime.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                    p.IsServed.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            ViewBag.SearchText = searchText;
            ViewBag.ServedPatients = servedPatients;
            ViewBag.NotServedPatients = notServedPatients;
            ViewBag.NextPatient = nextPatient;
            ViewBag.SecondNextPatient = secondNextPatient;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> NotifyNextPatient(int patientId)
        {
            var nextPatient = _queueDbContext.PatientBookings
                .Where(p => p.IsServed == "Not Yet Served" && p.Id != patientId)
                .OrderBy(p => p.BookingTime)
                .FirstOrDefault();

            if (nextPatient != null)
            {
                var isNotified = await _sendMail.SendEmailAsync(nextPatient.PatientName, nextPatient.ServiceType, nextPatient.EmailAddress, true);

                if (isNotified)
                {
                    TempData["success"] = "Next Patient Has Been Notified Via Email";
                }
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MarkAsServed(int patientId)
        {
            var patient = _queueDbContext.PatientBookings.Find(patientId);

            if (patient != null)
            {
                patient.IsServed = "Served";
                _queueDbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }

}
