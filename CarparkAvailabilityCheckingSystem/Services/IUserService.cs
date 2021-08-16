using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Models;

namespace CarparkAvailabilityCheckingSystem.Services
{
    //biz operations 
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> CreateUser(User user);

        Task<User> GetUser(Guid id);


    }
}
