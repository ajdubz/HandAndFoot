using HandFootLib.Services.Interfaces;
using HandFootLib.Models;
using Microsoft.AspNetCore.Mvc;
using HandFootLib.Models.DTOs.Player;
using System.Linq;

namespace HandAndFoot.Controllers
{
    [Route($"[controller]")]
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
                var oList = _playerService.GetPlayers();

                var oPlayers = oList.Select(x => new
                {
                    x.Id,
                    x.NickName,
                });

                return Ok(oPlayers.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet($"{{id:int}}")]
        public IActionResult GetPlayerDetails(int id)
        {
            try
            {
                var players = _playerService.GetPlayers();
                var oPlayer = players.Select(x => new
                {
                   x.Id,
                   x.NickName,

                }).SingleOrDefault(x => x.Id == id);

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


        [HttpPost($"account")]
        public IActionResult AddPlayer(PlayerSetAccountDTO playerSetAccountDTO)
        {
            try
            {
                _playerService.AddPlayer(playerSetAccountDTO);

                return Ok(StatusCode(200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet($"{{id:int}}/account")]
        public IActionResult GetPlayerAccount(int id)
        {
            try
            {
                var players = _playerService.GetPlayers();
                var oPlayer = players.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.Email,
                    x.Password,

                }).SingleOrDefault(x => x.Id == id);

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

        [HttpPut($"{{id:int}}/account")]
        public IActionResult UpdatePlayerAccount(int id, [FromBody] PlayerSetAccountDTO playerSetAccountDTO)
        {

            try
            {
                _playerService.UpdatePlayerAccount(id, playerSetAccountDTO);

                return Ok(StatusCode(200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        //[HttpPost($"{{id:int}}/friends")]
        //public IActionResult AddFriendRequest(int id, PlayerFriendBasicDTO playerFriend)
        //{
        //    try
        //    {
        //        if (id == playerFriend.PlayerId)
        //        {
        //            _playerService.AddFriend(playerFriend.PlayerId, playerFriend.FriendId);
        //        }
        //        else return NotFound();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}



        [HttpDelete($"{{id:int}}/account")]
        public IActionResult RemovePlayer(int id)
        {
            try
            {
                _playerService.RemovePlayer(id);
                return Ok(StatusCode(200));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //[HttpDelete($"{{id:int}}/friends")]
        //public IActionResult RemoveFriendRequest(int id, PlayerFriendBasicDTO playerFriend)
        //{
        //    try
        //    {
        //        if (id == playerFriend.PlayerId)
        //        {
        //            _playerService.RemoveFriend(playerFriend.PlayerId, playerFriend.FriendId);
        //        }
        //        else return NotFound();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

    }
}
