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
            }

            if (user.Contact.Length != 8)
            {
                throw new ApplicationException("Contact number should be 8 digits");
            }

            //check if firstname , lastname not empty 
            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {

                throw new ApplicationException("Please key in FirstName and LastName");
            }

            //check if firstname and lastname strictly alphabets 

            if (!Regex.IsMatch(user.FirstName, @"^[a-zA-Z]+$") || !Regex.IsMatch(user.LastName, @"^[a-zA-Z]+$"))
            {
                throw new ApplicationException("Please key in only alphabets for First and Last Name");
            }

            //email and password are present, contact optional
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException("Please key in password");
            }
            
            if (password.Length < 9)
            {
                throw new ApplicationException("Please key password more than 8 characters");
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
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


    }
}
