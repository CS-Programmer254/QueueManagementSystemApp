using System.ComponentModel.DataAnnotations;

namespace QueueMIS.Models
{
    public class PatientBooking
    {
        [Key]
        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateTime BookingTime { get; set; }
        public string ServiceType { get; set; }
        public string EmailAddress { get; set; }
        public string IsServed{ get; set; }

    }
}
