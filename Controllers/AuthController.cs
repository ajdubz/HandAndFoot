using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HandFootLib.Models.DTOs;
using HandFootLib.Services.Interfaces;

namespace HandAndFoot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService) => _userService = userService;


        [AllowAnonymous] // to skip validation
        [HttpPost("login")]
        public IActionResult Login (LoginGetDTO loginGetDTO)
        {

            try
            {
                var loginToken = _userService.Login(loginGetDTO);

                return Ok(loginToken);

            }
            catch (Exception ex)
            {
                return Unauthorized();


            }
        }
    }
}
