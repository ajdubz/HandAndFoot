using HandFootLib.Services.Interfaces;
using HandFootLib.Models;
using Microsoft.AspNetCore.Mvc;
using HandFootLib.Models.DTOs.Player;
using System.Linq;
using HandFootLib.Services;
using Microsoft.AspNetCore.Authorization;

namespace HandAndFoot.Controllers
{
    [Authorize]
    [Route($"[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        private readonly IPlayerService _playerService;
        private readonly IFriendsService _friendService;
        private readonly ILogger<PlayerController> _logger;
        public PlayerController(IPlayerService playerService, IFriendsService friendService, ILogger<PlayerController> logger)
        {
            _playerService = playerService;
            _friendService = friendService;
            _logger = logger;
        }


        //[AllowAnonymous] to skip validation
        [HttpGet]
        public IActionResult GetPlayersBasic()
        {
            try
            {
                //_logger.LogDebug("good in GetPlayersBasic");
                var oList = _playerService.GetPlayers();

                var oPlayers = oList.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.FullName,
                });

                return Ok(oPlayers.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Error in GetPlayersBasic");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet($"{{id:int}}")]
        public IActionResult GetPlayerDetails(int id)
        {
            try
            {
                //_logger.LogInformation("in GetPlayersBasic {id}", id);
                var players = _playerService.GetPlayers();
                var oPlayer = players.Select(x => new
                {
                   x.Id,
                   x.NickName,
                   x.FullName,

                }).SingleOrDefault(x => x.Id == id);

                if (oPlayer == null)
                {
                    return NotFound();
                }

                return Ok(oPlayer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPlayersBasic {id}", id);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost($"account")]
        public IActionResult AddPlayer(PlayerSetAccountDTO playerSetAccountDTO)
        {
            try
            {
                var newPlayer = _playerService.AddPlayer(playerSetAccountDTO);

                return Ok(newPlayer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost($"guest")]
        public IActionResult AddGuest(PlayerSetAccountDTO playerSetAccountDTO)
        {
            try
            {
                var newPlayer = _playerService.AddPlayer(playerSetAccountDTO);

                return Ok(newPlayer);
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
                    x.FullName,
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





        [HttpGet($"search/{{searchText}}")]
        public IActionResult SearchPlayer(string searchText)
        {

            try
            {
                var oList = _playerService.GetPlayers();

                var oPlayers = oList.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.FullName,

                }).Where(x => x.NickName != null && x.FullName != null && (x.NickName.Contains(searchText) || x.FullName.Contains(searchText)));

                return Ok(oPlayers.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet($"{{id:int}}/newFriendSearch/{{searchText}}")]
        public IActionResult SearchNewFriends(int id, string searchText)
        {

            try
            {
                var oList = _playerService.GetPlayers();


                var oPlayers = oList.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.FullName,

                }).Where(x => x.NickName != null && x.FullName != null && (x.NickName.Contains(searchText) || x.FullName.Contains(searchText)));


                var playerFriends = _friendService.GetFriends(id);

                oPlayers = oPlayers.Where(x => x.Id != id && !playerFriends.Any(pf => pf.Id == x.Id));

                return Ok(oPlayers.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet($"{{id:int}}/currFriendSearch/{{searchText}}")]
        public IActionResult SearchCurrentFriends(int id, string searchText)
        {

            try
            {
                var tempPlayers = _playerService.GetPlayers();
                var oPlayer = tempPlayers.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.FullName,

                }).SingleOrDefault(x => x.Id == id);

                if (oPlayer == null)
                {
                    return NotFound();
                }

                var oList = _friendService.GetFriends(id);

                //oList.Add(oPlayer);

                var oPlayers = oList.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.FullName,

                }).Where(x => x.NickName != null && x.FullName != null && (x.NickName.Contains(searchText) || x.FullName.Contains(searchText)));

                oPlayers.ToList().Add(oPlayer);

                return Ok(oPlayers);
            }
            catch (Exception ex)
            {
                // Log the exception message and stack trace
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, ex.Message);
            }


        }



        [HttpGet($"{{id:int}}/friends")]
        public IActionResult GetFriends(int id)
        {
            try
            {
                var getFriends = _friendService.GetFriends(id);

                var friends = getFriends.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.FullName,
                });

                return Ok(friends.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet($"{{id:int}}/friendRequests")]
        public IActionResult GetFriendRequests(int id)
        {
            try
            {
                var getFriendRequests = _friendService.GetFriendRequests(id);

                var friendRequests = getFriendRequests.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.FullName,
                });

                Console.WriteLine(id);

                return Ok(friendRequests.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet($"{{id:int}}/requestsSent")]
        public IActionResult GetFriendRequestsSent(int id)
        {
            try
            {
                var getFriendRequests = _friendService.GetFriendRequestsSent(id);

                var friendRequests = getFriendRequests.Select(x => new
                {
                    x.Id,
                    x.NickName,
                    x.FullName,
                });

                return Ok(friendRequests.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost($"{{id:int}}/requestAdd")]
        public IActionResult AddFriendRequest(int id, PlayerFriendBasicDTO playerFriendBasicDTO)
        {
            try
            {
                Console.WriteLine(playerFriendBasicDTO.FriendId);
                _friendService.AddFriendRequest(id, playerFriendBasicDTO.FriendId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut($"{{id:int}}/requestAccept")]
        public IActionResult AcceptFriendRequest(int id, PlayerFriendBasicDTO playerFriendBasicDTO)
        {
            try
            {
                _friendService.AcceptFriendRequest(playerFriendBasicDTO.FriendId, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
