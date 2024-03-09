using Microsoft.EntityFrameworkCore;
using QueueMIS.Models;

namespace QueueMIS.Services
{
    public class BookingManager : IBookingManager
    {   private readonly QueueDbContext _queueDbContext;
        public BookingManager(QueueDbContext queueDbContext) 
        { 
            _queueDbContext = queueDbContext;
        
        } 
        public async Task AddPatientBookingAsync(string patientName, DateTime bookingTime,string serviceType,string toEmail)
        {
            var booking = new PatientBooking
            {
                PatientName = patientName,
                BookingTime = bookingTime,
                ServiceType = serviceType,
                EmailAddress = toEmail,
                IsServed = "Not Yet Served"
            };

           await  _queueDbContext.PatientBookings.AddAsync(booking);
           await  _queueDbContext.SaveChangesAsync();
        }

        public int CountPatients()
        {
            int patientCount= _queueDbContext.PatientBookings.Count();
           return patientCount;
        }

        public async  Task<IEnumerable<PatientBooking>> GetPatientBookingsInQueueAsync()
        {
            var bookings = await _queueDbContext.PatientBookings
           .OrderBy(b => b.BookingTime)
           .ToListAsync();

            return bookings;
        }

        public int PatientsWithServiceType(string serviceType)
        {
            int patientWithServiceTypeCount = _queueDbContext.PatientBookings.Where(p=>p.ServiceType==serviceType).Count();
            return patientWithServiceTypeCount;

        }
    }
}

