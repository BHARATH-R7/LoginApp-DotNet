using LoginServiceWithJWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginServiceWithJWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private LoginServiceContext _context;
        public AuthController(LoginServiceContext context)
        {
            _context = context;
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

        }

        //https://localhost:7091/Auth/Login
        [HttpPost, Route("Login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid User Request");
            }


            if ((user.EmailId == "bharath@gmail.com" && user.Password == "A123"))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));

                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("Permission", "Write"),
                    new Claim("name", "Bharath"),
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7091",
                    audience: "http://localhost:4200",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(180),
                    signingCredentials: signingCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                var jsonResponse = new { Token = tokenString };

                //jsonResponse is a C# object, it will be parsed to Json by the Framework itself
                return Ok(jsonResponse);
            }
            else
            {
                var contextUser = _context.Users.Select(x => x.Email).ToList();
                if (contextUser.Contains(user.EmailId))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));

                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var role = _context.Users?.SingleOrDefault(x => x.Email == user.EmailId)?.Role;
                    var policy = RolesAndPolicies.Policies[role];

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, role),
                        new Claim("Permission", policy)
                    };

                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:7091",
                        audience: "https://localhost:7091",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(50),
                        signingCredentials: signingCredentials );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    var jsonResponse = new { Token = tokenString };

                    //jsonResponse is a C# object, it will be parsed to Json by the Framework itself
                    return Ok(jsonResponse);
                }
            }
            return Unauthorized();
        }
    }
}
