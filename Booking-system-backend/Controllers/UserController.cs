using Booking_system_backend.Context;
using Booking_system_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booking_system_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

     public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext appDbContext) 
        {
            _authContext = appDbContext;

        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _authContext.Users
                .FirstOrDefaultAsync(x => x.Email == userObj.Email  && x.Password == userObj.Password);
            if(user == null)
                return NotFound(new { message = "User Not Found!" });
            return Ok(new
            {
                message = "Login Success!"
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            // Convert name, email, and cellphone to lowercase for case-insensitive comparison
            string name = userObj.Name.ToLower();
            string email = userObj.Email.ToLower();
            string cellphone = userObj.Cellphone.ToLower();

            // Check if the user already exists in the database based on name, email, and cellphone
            var existingUser = await _authContext.Users.FirstOrDefaultAsync(u =>
                u.Name.ToLower() == name &&
                u.Email.ToLower() == email &&
                u.Cellphone.ToLower() == cellphone);

            if (existingUser != null)
            {
                return BadRequest(new
                {
                    Message = "User already exists!"
                });
            }

            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "User Registered!"
            });
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            // Check if the user exists in the database based on their email
            var user = await _authContext.Users.FirstOrDefaultAsync(u =>
                u.Email.ToLower() == userObj.Email.ToLower());

            if (user == null)
            {
                return NotFound(new
                {
                    Message = "User not found!"
                });
            }

            // Update the user's password in the database
            user.Password = userObj.Password;
            _authContext.Users.Update(user);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Password reset successful!"
            });
        }

    }

}
