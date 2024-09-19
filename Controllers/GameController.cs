using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Models.DTOs.Game;
using HandFootLib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandAndFoot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }



        [HttpGet]
        public IActionResult GetAllGames()
        {
            try
            {
                var games = _gameService.GetGames();
                return Ok(games.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting games");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet($"{{id:int}}")]
        public IActionResult GetGame(int id) {
            try
            {
                var game = _gameService.GetGames().SingleOrDefault(g => g.Id == id);
                //_logger.LogWarning(game?.Id.ToString());
                return Ok(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting game");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet($"{{gameId:int}}/team")]
        public IActionResult GetTeamsByGameId(int gameId)
        {
            try
            {
                _logger.LogWarning(gameId.ToString());

                var teams = _gameService.GetTeamsByGameId(gameId);
                return Ok(teams);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting teams by game id");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet($"{{id:int}}/round")]
        public IActionResult GetRoundsByGameId(int gameId)
        {
            try
            {
                var rounds = _gameService.GetRoundsByGameId(gameId);
                return Ok(rounds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting rounds by game id");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddGame(GameAddDTO gameAddDTO) {
            try
            {
                var game = _gameService.AddGame(gameAddDTO);

                //_logger.LogWarning(message: $"{game.Id} - {game.Rules?.CleanBookScore.ToString()}");


                return Ok(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding game");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost($"{{gameId:int}}/team/{{teamId}}")]
        public IActionResult AddTeamToGame(int gameId, int teamId)
        {
            try
            {
                _gameService.AddTeamToGame(gameId, teamId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding team to game");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



    }
}
