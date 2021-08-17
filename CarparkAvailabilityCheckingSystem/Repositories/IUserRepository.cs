using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Entities;

namespace CarparkAvailabilityCheckingSystem.Repositories
{
    //db operations 
    public interface IUserRepository
    {
        Task<IEnumerable<User>> Get();
        Task<User> Create(User user);
        Task<User> Get(Guid id);

        bool CheckEmailExist(User user);

    }
}
