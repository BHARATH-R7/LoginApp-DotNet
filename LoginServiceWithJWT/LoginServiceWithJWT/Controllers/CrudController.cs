using LoginServiceWithJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginServiceWithJWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    //[Authorize(Policy = "ReadPermission")]
    //[Authorize(Policy = "WritePermission")]
    public class CrudController : ControllerBase
    {
        private LoginServiceContext _context;

        public CrudController(LoginServiceContext context)
        {
            _context = context;
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

        }

        //1) [HttpGet("get")]
        //2) [HttpGet, Route("get")]  both works same


        //https://localhost:7091/crud/get
        [HttpGet("get")]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return _context.Users;
        }

        //https://localhost:7091/crud/get/1
        [HttpGet("get/{id}")]
        public ActionResult<User> GetUserById([FromRoute] int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        //https://localhost:7091/crud/post
        [HttpPost("post")]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            user.Id = _context.Users.Count() + 1;
            _context.Users.Add(user);
            _context.SaveChanges();

            return NoContent();
        }

        //https://localhost:7091/crud/put/1
        [HttpPut("put/{id}")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            _context.SaveChanges();

            return Ok();
        }

        //https://localhost:7091/crud/delete/1
        [Authorize(Roles = "Admin", Policy = "WritePermission")]
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
