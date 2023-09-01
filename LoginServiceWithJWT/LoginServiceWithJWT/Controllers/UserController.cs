using LoginServiceWithJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginServiceWithJWT.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var Users = new List<string>() { "Bharath", "Sharath" };
            return Ok(Users);
        }


        [HttpGet, Route("Games")]
        public IActionResult GetGames()
        {
            var Games = new List<string>() { "Cricket", "Football", "Basketball", "UFC" };
            return Ok(Games);
        }

        [HttpGet, Route("Games/{id}")]
        public IActionResult GetGames([FromRoute] int id)
        {
            var Games = new List<GamesModel>()
            {
                new GamesModel() { Id = 1, Name = "Cricket" },
                new GamesModel() { Id = 2, Name = "Football" },
                new GamesModel() { Id = 3, Name = "Basketball" },
                new GamesModel() { Id = 4, Name = "UFC" },
            };

            var selectedGame = Games.FirstOrDefault(x => x.Id == id);

            return Ok(selectedGame);
        }
    }
}
