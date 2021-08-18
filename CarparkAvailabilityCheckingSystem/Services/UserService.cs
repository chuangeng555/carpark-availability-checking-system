using CarparkAvailabilityCheckingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarparkAvailabilityCheckingSystem.Repositories;
using System.Text.RegularExpressions;

namespace CarparkAvailabilityCheckingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }


        /** 
         * Check if FirstName, LastName, Email and Password is not empty
         * If Contact exist, make sure it is numeric and exactly 8 digits 
         * Check if FirstName and LastName contains alphabets only 
         * Check for alphanumeric characters (at least 1 alph and 1 numerical) and mininum 8 characters for password
         * Check for Valid email & if user email exist in database or not
         * **/
        public async Task<User> CreateUser(User user, string password)
        {

            if (user == null)
            {
                throw new ApplicationException("Registration is empty");
            }

            if (!string.IsNullOrWhiteSpace(user.Contact))
            {
                if (!IsAllDigits(user.Contact))
                {
                    throw new ApplicationException("Please key in only numbers for contact");
                }

                if (user.Contact.Length != 8)
                {
                    throw new ApplicationException("Contact number should be 8 digits");
                }
            }


            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {

                throw new ApplicationException("Please key in FirstName and LastName");
            }


            if (!Regex.IsMatch(user.FirstName, @"^[a-zA-Z]+$") || !Regex.IsMatch(user.LastName, @"^[a-zA-Z]+$"))
            {
                throw new ApplicationException("Please key in only alphabets for First and Last Name");
            }
            
            if (password.Length < 8)
            {
                throw new ApplicationException("Please key 8 characters or more for password");
            }

            if (string.IsNullOrWhiteSpace(password) || password == null) throw new ApplicationException("Password cannot be empty");

            if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*\d)"))
            {
                throw new ApplicationException("Please key in alphanumeric characters for password");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ApplicationException("Please key in Email");
            }
            else
            {
                //check if email is valid
                if (!IsValidEmail(user.Email))
                {
                    throw new ApplicationException("Email invalid");
                }

                //check email already added or not 
                if (_userRepo.CheckEmailExist(user))
                    throw new ApplicationException("Email " + user.Email + " is already taken");

            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var createdUser = await _userRepo.Create(user);

            return createdUser;
        }

        bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }


        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public string CreateUserValidation(User user)
        {

            return "Approved"; 
        }

        public async Task<User> GetUser(Guid id) {
            var GetUserId = await _userRepo.Get(id);
            return GetUserId;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepo.Get();

        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {


            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepo.GetUser(email);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }
}
