using CarparkAvailabilityCheckingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarparkAvailabilityCheckingSystem.Services
{
    public interface ICarparkService
    {
        Task<CarparkModel> GetCarparkAvailability();
    }
}
