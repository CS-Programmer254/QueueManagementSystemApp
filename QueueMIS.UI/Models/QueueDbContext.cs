using Microsoft.EntityFrameworkCore;

namespace QueueMIS.Models
{
    public class QueueDbContext : DbContext
    {
        public QueueDbContext(DbContextOptions<QueueDbContext>options):base(options)
        {
    
        }
        public DbSet<PatientBooking> PatientBookings { get; set; }

    }
}
