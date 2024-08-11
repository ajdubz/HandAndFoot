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

        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService) { _playerService = playerService; }


        [HttpGet]
        public IActionResult GetPlayers()
        {
            var oList = _playerService.GetPlayers();

            return Ok(oList.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        {
            var oPlayer = _playerService.GetPlayer(id);

            return Ok(oPlayer);
        }

        [HttpPost]
        public IActionResult AddPlayer(PlayerCreateDTO PlayerCreateDTO)
        {
            var oPlayer = new Player();

            oPlayer.NickName = PlayerCreateDTO.NickName;
            oPlayer.Email = PlayerCreateDTO.Email;
            oPlayer.Password = PlayerCreateDTO.Password;

            _playerService.AddPlayer(oPlayer);

            return Ok("Added Successfully");
        }

        [HttpPut]
        public IActionResult UpdatePlayer(Player player)
        {

            _playerService.UpdatePlayer(player);

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            _playerService.DeletePlayer(id);

            return Ok("Deleted Successfully");
        }

    }
}
