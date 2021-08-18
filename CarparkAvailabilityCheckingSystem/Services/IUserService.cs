using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Entities;

namespace CarparkAvailabilityCheckingSystem.Services
{
    //biz operations 
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> CreateUser(User user, string password);

        Task<User> GetUser(Guid id);

        User Authenticate(string email, string password);

    }
}
