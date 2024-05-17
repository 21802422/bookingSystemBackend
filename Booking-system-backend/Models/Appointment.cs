using System;
using System.ComponentModel.DataAnnotations;

namespace Booking_system_backend.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        public int ForeignId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime RescheduledDate { get; set; }
        public string TimeSlot { get; set; }
        public string Length { get; set; }
        public string HairstyleType { get; set; }
        public int Status { get; set; }
        public double Price { get; set; }
    }
}