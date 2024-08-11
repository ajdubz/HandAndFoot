using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandAndFoot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IPlayerService _playerService;

        public TeamController(ITeamService teamService, IPlayerService playerService)
        {
            _teamService = teamService;
            _playerService = playerService;
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            return Ok(_teamService.GetTeams());
        }

        [HttpGet("{id}")]
        public IActionResult GetTeam(int id)
        {
            return Ok(_teamService.GetTeam(id));
        }

        //[HttpPost]
        //public IActionResult AddTeam(TeamDTO teamDTO)
        //{
        //    var team = new Team();
        //    team.Name = teamDTO.Name;

        //    var oListIds = teamDTO.PlayerIds.ToList();

        //    foreach (var id in oListIds)
        //    {
        //        var player = _playerService.GetPlayer(id);
        //        if (player != null)
        //        {
        //            team.Players.Add(player);
        //        }
        //    }

        //    _teamService.AddTeam(team);

        //    return Ok("Added Successfully");
        //}

        [HttpPut]
        public IActionResult UpdateTeam(Team team)
        {
            _teamService.UpdateTeam(team);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            _teamService.DeleteTeam(id);
            return Ok("Deleted Successfully");
        }
    }
}
