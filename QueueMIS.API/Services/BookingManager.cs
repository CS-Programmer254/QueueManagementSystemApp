using Microsoft.EntityFrameworkCore;
using QueueMIS.API.Models;

namespace QueueMIS.API.Services
{
    public class BookingManager : IBookingManager
    {   private readonly QueueDbContext _queueDbContext;
        public BookingManager(QueueDbContext queueDbContext) 
        { 
            _queueDbContext = queueDbContext;
        
        } 
        public async Task AddPatientBookingAsync(string patientName, DateTime bookingTime)
        {
            var booking = new PatientBooking
            {
                PatientName = patientName,
                BookingTime = bookingTime
            };

           await  _queueDbContext.PatientBookings.AddAsync(booking);
           await  _queueDbContext.SaveChangesAsync();
        }

        public async  Task<IEnumerable<PatientBooking>> GetPatientBookingsInQueueAsync()
        {
            var bookings = await _queueDbContext.PatientBookings
           .OrderBy(b => b.BookingTime)
           .ToListAsync();

            return bookings;
        }
    }
}
