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

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
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



        [HttpPut("AddPlayerToTeam")]
        public IActionResult AddPlayerToTeam(int playerId, int teamId)
        {
            _teamService.AddPlayerToTeam(playerId, teamId);
            return Ok("Added Successfully");
        }

        [HttpPut("RemovePlayerFromTeam")]
        public IActionResult RemovePlayerFromTeam(int playerId, int teamId)
        {
            _teamService.RemovePlayerFromTeam(playerId, teamId);
            return Ok("Removed Successfully");
        }
        

        
        [HttpDelete("{id:int}")]
        public IActionResult RemoveTeam(int id)
        {
            _teamService.RemoveTeam(id);
            return Ok("Deleted Successfully");
        }
    }
}
