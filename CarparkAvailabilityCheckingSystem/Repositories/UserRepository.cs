using CarparkAvailabilityCheckingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarparkAvailabilityCheckingSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            user.Id = Guid.NewGuid();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<User> Get(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool CheckEmailExist(User user)
        {
            return _context.Users.Any(x => x.Email == user.Email);
        }

        public User GetUser(string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email== email);
            return user;
        }

    }
}
