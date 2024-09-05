using HandFootLib.Models;
using HandFootLib.Models.DTOs.Player;
using HandFootLib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandAndFoot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendsService _friendService;
        public FriendController(IFriendsService friendService) => _friendService = friendService;

        [HttpGet($"{{id:int}}")]
        public IActionResult GetFriends(int id)
        {
            try
            {
                var getFriends = _friendService.GetFriends(id);

                var friends = getFriends.Select(x => new
                {
                    x.Id,
                    x.NickName,
                });

                return Ok(friends.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet($"{{id:int}}/requests")]
        public IActionResult GetFriendRequests(int id)
        {
            try
            {
                var getFriendRequests = _friendService.GetFriendRequests(id);

                var friendRequests = getFriendRequests.Select(x => new
                {
                    x.Id,
                    x.NickName,
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
               _friendService.AddFriendRequest(id, playerFriendBasicDTO.FriendId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost($"{{id:int}}/requestAccept")]
        public IActionResult AcceptFriendRequest(int id, PlayerFriendBasicDTO playerFriendBasicDTO)
        {
            try
            {
                _friendService.AcceptFriendRequest(playerFriendBasicDTO.PlayerId, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost]
        //public IActionResult RemoveFriendRequest()
        //{
        //    try
        //    {
        //        // RemoveFriendRequest logic here

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public IActionResult DeclineFriendRequest()
        //{
        //    try
        //    {
        //        // DeclineFriendRequest logic here

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public IActionResult RemoveFriend()
        //{
        //    try
        //    {
        //        // RemoveFriend logic here

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
