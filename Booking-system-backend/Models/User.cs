using System;
using System.ComponentModel.DataAnnotations;

namespace Booking_system_backend.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        public string Password { get; set; }
    }
}
