﻿using Microsoft.AspNetCore.Authorization;
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
using HandFootLib.Services.Interfaces;
using HandFootLib.Models.DTOs.Login;

namespace HandAndFoot.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [AllowAnonymous] // to skip validation
        [HttpPost("login")]
        public IActionResult Login (LoginGetDTO loginGetDTO)
        {

            try
            {
                var loginFoundUser = _userService.Login(loginGetDTO);



                return Ok(new { loginFoundUser.Id, loginFoundUser.NickName, loginFoundUser.Email, loginFoundUser.Token });

            }
            catch (Exception ex)
            {
                return Unauthorized();


            }
        }
    }
}
