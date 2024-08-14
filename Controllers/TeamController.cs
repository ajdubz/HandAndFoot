using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Models.DTOs.Team;
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
        //private readonly IPlayerService _playerService;

        public TeamController(ITeamService teamService, IPlayerService playerService)
        {
            _teamService = teamService;
            //_playerService = playerService;
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            return Ok(_teamService.GetTeams());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTeam(int id)
        {
            return Ok(_teamService.GetTeam(id));
        }

        [HttpPost]
        public IActionResult AddTeam(TeamCreateDTO teamDTO)
        {
            _teamService.AddTeam(teamDTO);

            return Ok("Added Successfully");
        }

        [HttpPut]
        public IActionResult UpdateTeam(TeamUpdateDTO team)
        {
            _teamService.UpdateTeam(team);
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTeam(int id)
        {
            _teamService.RemoveTeam(id);
            return Ok("Deleted Successfully");
        }
    }
}
