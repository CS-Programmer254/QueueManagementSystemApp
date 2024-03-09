using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueueMIS.API.Services;

namespace QueueMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientBookingsController : ControllerBase
    {
        private readonly BookingManager _bookingManager;

        public PatientBookingsController(BookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientBookings()
        {
            var bookings = await _bookingManager.GetPatientBookingsInQueueAsync();
            return Ok(bookings);
        }
    }
}
