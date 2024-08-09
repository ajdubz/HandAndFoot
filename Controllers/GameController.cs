using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandAndFoot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            return Ok(_gameService.GetGames());
        }

        [HttpGet("{id}")]
        public IActionResult GetGame(int id)
        {
            return Ok(_gameService.GetGame(id));
        }

        [HttpPost]
        public IActionResult AddGame(GameDTO gameDTO)
        {
            var game = new Game();
            game.Teams = gameDTO.Teams;

            return Ok(_gameService.AddGame(game));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGame(Game game)
        {
            return Ok(_gameService.UpdateGame(game));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            _gameService.DeleteGame(id);
            return Ok();
        }
    }
}
