using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Models;
using CarparkAvailabilityCheckingSystem.Entities;
using AutoMapper;

namespace CarparkAvailabilityCheckingSystem.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegistrationModel, User>();
        }
    }
}
