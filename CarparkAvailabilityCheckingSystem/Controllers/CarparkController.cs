using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Services;
using CarparkAvailabilityCheckingSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarparkAvailabilityCheckingSystem.Controllers
{
    [Authorize]
    [Route("api/carpark")]
    [ApiController]
    public class CarparkController : ControllerBase
    {
        private readonly ICarparkService _carparkService;

        public CarparkController(ICarparkService carparkService)
        {
            _carparkService = carparkService;
        }

        [HttpGet("getCarparkAvailability")]
        public async Task<CarparkModel> GetUsers()
        {
            var result = await _carparkService.GetCarparkAvailability();
            return result; 
        }


    }
}
