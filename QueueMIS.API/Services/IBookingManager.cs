using QueueMIS.API.Models;

namespace QueueMIS.API.Services
{
    public interface IBookingManager
    {
        public Task AddPatientBookingAsync(string patientName, DateTime bookingTime);
        public Task<IEnumerable<PatientBooking>> GetPatientBookingsInQueueAsync();

    }
}
