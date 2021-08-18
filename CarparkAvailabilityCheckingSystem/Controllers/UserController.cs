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


namespace CarparkAvailabilityCheckingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper; 
            _userService = userService;
        }


        [HttpGet]
        public async Task<IList<UserModel>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            var model = _mapper.Map<IList<UserModel>>(users);
            return model;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            return await _userService.GetUser(id);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] RegistrationModel model)
        {

            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                var newUser = await _userService.CreateUser(user, model.Password);
                return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
            } catch(ApplicationException ae)
            {
                return BadRequest(new { message = ae.Message });
            }
        }


    }
}
