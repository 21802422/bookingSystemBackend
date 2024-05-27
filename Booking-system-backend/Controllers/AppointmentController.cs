using Microsoft.AspNetCore.Mvc;
using Booking_system_backend.Context;
using Booking_system_backend.Models;


namespace Booking_system_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _appContext;

        public AppointmentController(AppDbContext appDbContext)
        {
            _appContext = appDbContext;
        }

        [HttpPost]
        public IActionResult CreateAppointment(Appointment appointment)
        {
            try
            {
                appointment.UpdatedDate = DateTime.Now;
                appointment.CreatedDate = DateTime.Now;
                _appContext.Appointments.Add(appointment);
                _appContext.SaveChanges();
                return Ok(new GenericResponse
                {
                    Message = "Appointment created successfully",
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating appointment: {ex.Message}");
            }
        }
    }
}

