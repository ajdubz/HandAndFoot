using HandFootLib.Services.Interfaces;
using HandFootLib.Models;
using Microsoft.AspNetCore.Mvc;
using HandFootLib.Models.DTOs.Player;

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

        [HttpGet("{id:int}")]
        public IActionResult GetPlayer(int id)
        {
            var oPlayer = _playerService.GetPlayer(id);

            return Ok(oPlayer);
        }

        [HttpGet("Players")]
        public IActionResult GetPlayersWithFriends()
        {
            var oList = _playerService.GetPlayersWithFriends();

            return Ok(oList.ToList());
        }

        [HttpGet("Players/{id:int}")]
        public IActionResult GetPlayerWithFriends(int id)
        {
            var oPlayer = _playerService.GetPlayerWithFriends(id);

            return Ok(oPlayer);
        }

        [HttpPost]
        public IActionResult AddPlayer(PlayerCreateDTO playerCreateDTO)
        {
            var oPlayer = new Player
            {
                NickName = playerCreateDTO.NickName,
                Email = playerCreateDTO.Email,
                Password = playerCreateDTO.Password,
            };

            _playerService.AddPlayer(oPlayer);

            return Ok("Added Successfully");
        }

        [HttpPut]
        public IActionResult UpdatePlayer(PlayerUpdateDTO playerUpdateDTO)
        {

            _playerService.UpdatePlayer(playerUpdateDTO);

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id:int}")]
        public IActionResult RemovePlayer(int id)
        {
            _playerService.RemovePlayer(id);

            return Ok("Removed Successfully");
        }

        [HttpPost(nameof(AddFriend))]
        public IActionResult AddFriend(PlayerFriendBasicDTO playerFriend)
        {

            _playerService.AddFriend(playerFriend.PlayerId, playerFriend.FriendId);

            return Ok("Added Successfully");
        }

        [HttpDelete(nameof(RemoveFriend))]
        public IActionResult RemoveFriend(PlayerFriendBasicDTO playerFriend)
        {
            _playerService.RemoveFriend(playerFriend.PlayerId, playerFriend.FriendId);

            return Ok("Removed Successfully");
        }

    }
}
