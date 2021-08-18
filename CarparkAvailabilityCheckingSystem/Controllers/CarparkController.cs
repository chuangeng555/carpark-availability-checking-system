using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Services;
using CarparkAvailabilityCheckingSystem.Models;

namespace CarparkAvailabilityCheckingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarparkController : ControllerBase
    {
        private readonly ICarparkService _carparkService;

        public CarparkController(ICarparkService carparkService)
        {
            _carparkService = carparkService;
        }

        [HttpGet]
        public async Task<CarparkModel> GetUsers()
        {
            var result = await _carparkService.GetCarparkAvailability();
            return result; 
        }


    }
}
