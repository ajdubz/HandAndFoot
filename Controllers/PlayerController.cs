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
            try
            {
                var oList = _playerService.GetPlayers();
                return Ok(oList.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPlayer(int id)
        {
            try
            {
                var oPlayer = _playerService.GetPlayer(id);
                if (oPlayer == null)
                {
                    return NotFound();
                }
                return Ok(oPlayer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPut]
        public IActionResult UpdatePlayer(PlayerUpdateDTO playerUpdateDTO)
        {
            try
            {
                _playerService.UpdatePlayer(playerUpdateDTO);
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        public IActionResult AddPlayer(PlayerCreateDTO playerCreateDTO)
        {
            try
            {
                var oPlayer = new Player
                {
                    NickName = playerCreateDTO.NickName,
                    Email = playerCreateDTO.Email,
                    Password = playerCreateDTO.Password,
                };

                _playerService.AddPlayer(oPlayer);

                return Ok(StatusCode(200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost(nameof(AddFriend))]
        public IActionResult AddFriend(PlayerFriendBasicDTO playerFriend)
        {
            try
            {
                _playerService.AddFriend(playerFriend.PlayerId, playerFriend.FriendId);
                return Ok("Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpDelete("{id:int}")]
        public IActionResult RemovePlayer(int id)
        {
            try
            {
                _playerService.RemovePlayer(id);
                return Ok("Removed Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete(nameof(RemoveFriend))]
        public IActionResult RemoveFriend(PlayerFriendBasicDTO playerFriend)
        {
            try
            {
                _playerService.RemoveFriend(playerFriend.PlayerId, playerFriend.FriendId);
                return Ok("Removed Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
