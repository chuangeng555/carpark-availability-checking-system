using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Services;
using CarparkAvailabilityCheckingSystem.Models;
using CarparkAvailabilityCheckingSystem.Entities;
using CarparkAvailabilityCheckingSystem.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Claims;

namespace CarparkAvailabilityCheckingSystem.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper; 
            _userService = userService;
            _appSettings = appSettings.Value;
        }


        [HttpGet("getMembersDetail")]
        public async Task<IList<UserModel>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            var model = _mapper.Map<IList<UserModel>>(users);
            return model;
        }

        //once authenticate, issue jwt token expiry date 7 days
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return authentication token
            return Ok(new
            {
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<ActionResult<User>> PostUser([FromBody] RegistrationModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                var newUser = await _userService.CreateUser(user, model.Password);
                return Ok(newUser.Id + " User Created");
            } catch(ApplicationException ae)
            {
                return BadRequest(new { message = ae.Message });
            }
        }


    }
}
