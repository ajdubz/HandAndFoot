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
        public PlayerController(IPlayerService playerService) => _playerService = playerService;


        [HttpGet]
        public IActionResult GetPlayersBasic()
        {
            try
            {
                var oList = _playerService.GetPlayersBasic();

                return Ok(oList.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetPlayerDetails(int id)
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


        [HttpPost]
        public IActionResult AddPlayer(PlayerSetAccountDTO playerSetAccountDTO)
        {
            try
            {

                _playerService.AddPlayer(playerSetAccountDTO);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}/account")]
        public IActionResult GetPlayerAccount(int id)
        {
            try
            {
                var oPlayer = _playerService.GetPlayerAccount(id);

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

        // Why [FromBody] here?
        [HttpPut("{id:int}/account")]
        public IActionResult UpdatePlayerAccount(int id, PlayerSetAccountDTO playerSetAccountDTO)
        {

            try
            {
                _playerService.UpdatePlayerAccount(id, playerSetAccountDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine(playerSetAccountDTO);
                return StatusCode(500, ex.Message);
            }
        }






        [HttpPost(nameof(AddFriend))]
        public IActionResult AddFriend(PlayerFriendBasicDTO playerFriend)
        {
            try
            {
                _playerService.AddFriend(playerFriend.PlayerId, playerFriend.FriendId);
                return Ok();
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
                return Ok();
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
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
