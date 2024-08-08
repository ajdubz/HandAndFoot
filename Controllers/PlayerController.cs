using HandFootLib.Services.Interfaces;
using HandFootLib.Models;
using Microsoft.AspNetCore.Mvc;
using HandFootLib.Models.DTOs;

namespace HandAndFoot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        IPlayerService _playerService;
        public PlayerController(IPlayerService playerService) { _playerService = playerService; }


        [HttpGet]
        public IActionResult GetPlayers()
        {
            var oList = _playerService.GetPlayers();

            return Ok(oList);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        {
            var oPlayer = _playerService.GetPlayer(id);

            return Ok(oPlayer);
        }

        [HttpPost]
        public IActionResult AddPlayer(PlayerDTO playerDTO)
        {
            var oPlayer = new Player();

            oPlayer.Name = playerDTO.Name;
            oPlayer.NickName = playerDTO.NickName;

            oPlayer = _playerService.AddPlayer(oPlayer);

            return Ok(oPlayer);
        }

    }
}
