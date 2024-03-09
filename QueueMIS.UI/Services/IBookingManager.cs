using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueueMIS.Models;

namespace QueueMIS.Services
{
    public interface IBookingManager
    {
        public Task AddPatientBookingAsync(string patientName, DateTime bookingTime,string serviceType,string toEmail);
        public Task<IEnumerable<PatientBooking>> GetPatientBookingsInQueueAsync();
        public int CountPatients();
        public int PatientsWithServiceType(string serviceType);
      
    }
}
