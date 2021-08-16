using CarparkAvailabilityCheckingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Repositories;

namespace CarparkAvailabilityCheckingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> CreateUser(User user)
        {
            var createdUser = await _userRepo.Create(user);
            return createdUser;
        }

        public async Task<User> GetUser(Guid id) {
            var GetUserId = await _userRepo.Get(id);
            return GetUserId;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepo.Get();
        }


    }
}
