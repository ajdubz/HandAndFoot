using HandFootLib.Models;
using HandFootLib.Models.DTOs;
using HandFootLib.Models.DTOs.Team;
using HandFootLib.Services;
using HandFootLib.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandAndFoot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILogger<TeamController> _logger;

        public TeamController(ITeamService teamService, ILogger<TeamController> logger)
        {
            _teamService = teamService;
            _logger = logger;
        }


        [HttpGet]   
        public IActionResult GetTeams()
        {
            try
            {
                var teams = _teamService.GetTeams();

                var teamsList = teams.Select(x => new { x.Id, x.Name });


                return Ok(teamsList.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("Names")]
        public IActionResult GetTeamsWithPlayerNames()
        {
            try
            {
                var teams = _teamService.GetTeamsWithPlayerNames();

                return Ok(teams.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult GetTeam(int id)
        {
            try
            {
                var teams = _teamService.GetTeams();

                var teamsList = teams.Select(x => new { x.Id, x.Name });

                var team= teamsList.SingleOrDefault(x => x.Id == id);

                if (team == null)
                {
                    return NotFound();
                }

                return Ok(teamsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        
        [HttpPost]
        public IActionResult AddTeam(TeamCreateDTO teamDTO)
        {
            try
            {
                var newTeam = _teamService.AddTeam(teamDTO);

                //_logger.LogWarning(teamDTO.Id.ToString());

                return Ok(newTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Player")]
        public IActionResult AddPlayersToTeam(AddPlayersToTeamDTO addPlayersToTeam)
        {
            try
            {
                var playerTeam = _teamService.AddPlayersToTeam(addPlayersToTeam);

                return Ok(playerTeam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet($"search/{{searchText}}")]
        public IActionResult SearchTeams(string searchText)
        {

            try
            {
                var oList = _teamService.GetTeams();

                var oTeams = oList.Select(x => new
                {
                    x.Id,
                    x.Name
                }).Where(x => x.Name != null && x.Name.Contains(searchText));

                return Ok(oTeams.ToList());
            }
            catch (Exception ex)
            {
                // Handle the exception here
                // You can log the exception or perform any other necessary actions
                // For example:
                _logger.LogError(ex, "An error occurred while searching for teams");
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet($"search/{{inId}}-{{searchText}}")]
        public IActionResult SearchPlayerTeams(int inId, string searchText)
        {

            try
            {
                var oList = _teamService.GetPlayerTeams(inId);

                var oTeams = oList.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.TeamMembers
                }).Where(x => x.Name != null && x.Name.Contains(searchText));

                return Ok(oTeams.ToList());
            }
            catch (Exception ex)
            {
                // Handle the exception here
                // You can log the exception or perform any other necessary actions
                // For example:
                _logger.LogError(ex, "An error occurred while searching for teams");
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet($"Players/{{playerIds}}")]
        public IActionResult GetTeamByPlayerIds([FromQuery] GetTeamByPlayerIds playerIds)
        {
            try
            {
                var teams = _teamService.GetTeamByPlayerIds(playerIds);

                var teamsList = teams.Select(x => new { x.Id, x.Name });

                return Ok(teamsList.ToList());

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
