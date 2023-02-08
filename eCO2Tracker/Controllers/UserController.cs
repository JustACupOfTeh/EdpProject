using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using eCO2Tracker.Utility;
using Newtonsoft.Json;
using System.Text;
using Azure;

namespace eCO2Tracker.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("list")]
        public ActionResult<List<User>> GetAllUsers()
        {
            try
            {
                List<User> list = _userService.GetAll();
                return Ok(list);
            }
            catch (Exception)
            {
                return BadRequest("Technical Error");
            }
        }

        [HttpPost("login")]
        public ActionResult<User> Login(User user)
        {
            try
            {
                User u = new()
                {
                    Email = user.Email,
                    Password = user.Password
                };

                User? existingUser = _userService.Login(u);
                if (existingUser != null)
                {
                    return Ok(existingUser);
                }
                else
                {
                    return BadRequest("Technical Error");
                }
            }
            catch (Exception)
            {
                return BadRequest("Technical Error");
            }
        }

        [HttpPost("register")]
        public ActionResult CreateNewUser(User newUser)
        {
            try
            {
                string hashedPassword = Hashing.HashPassword(newUser.Password);
                string referralCode = CodeGenerator.GetReferralCode();

                User u = new()
                {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    Password = hashedPassword,
                    ReferralCode = referralCode,
                    Role = "User",
                    CreatedDate = DateTime.Now
                };
                _userService.CreateUser(u);
                return Created("", newUser);

            }
            catch (Exception)
            {
                return BadRequest("Technical Error");
            }
        }

        [HttpGet("view")]
        public ActionResult ViewUser([FromQuery] string id)
        {
            try
            {
                User? user = _userService.GetUserById(id);
                user.Password = "";

                return Ok(user);
            }
            catch (Exception)
            {
                return BadRequest("Technical Error");
            }
        }


        [HttpPut("update")]
        //public ActionResult UpdateUser([FromBody] User user)
        public ActionResult UpdateUser(User user)
        {
            try
            {
                string hashedPassword = Hashing.HashPassword(user.Password);

                User u = new()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = hashedPassword,
                    UpdatedDate = DateTime.Now
                };
                _userService.UpdateUser(u);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest("Technical Error");
            }
        }

        [HttpGet("getReferralCode")]
        public ActionResult retrieveReferralCode([FromQuery] string id)
        {
            try
            {
                string referralCode = _userService.GetReferralCodeById(id);
                return Ok(referralCode);
            }
            catch (Exception)
            {
                return BadRequest("Technical Error");
            }
        }

        [HttpDelete("delete")]
        public ActionResult DeleteUser([FromQuery] string id)
        {
            try
            {
                User u = new()
                {
                    Id = Guid.Parse(id)
                };

                _userService.DeleteUser(u);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Technical Error");
            }
        }

    }
}
