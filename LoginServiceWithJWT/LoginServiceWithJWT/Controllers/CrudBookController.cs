using LoginServiceWithJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoginServiceWithJWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CrudBookController : ControllerBase
    {
        private LoginServiceContext _context;

        public CrudBookController(LoginServiceContext context)
        {
            _context = context;
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

        }

        //https://localhost:7091/crudbook/get
        [HttpGet("get")]
        public ActionResult<IEnumerable<Book>> GetAllUsers()
        {
            return _context.Books;
        }


        //https://localhost:7091/crudbook/post
        [HttpPost("post")]
        public ActionResult<Book> CreateUser([FromBody] Book book)
        {
            //book.Id = _context.Books.Count() + 1;
            _context.Books.Add(book);
            _context.SaveChanges();

            return Ok(book);
        }

        //https://localhost:7091/crudbook/put/1
        [HttpPut("put/{id}")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(u => u.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Name = book.Name;
            _context.SaveChanges();

            return Ok(existingBook);
        }

        //https://localhost:7091/crudbook/delete/1
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var book = _context.Books.FirstOrDefault(u => u.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok(book);
        }
    }
}
