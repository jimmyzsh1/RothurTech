using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        public readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> RegisterUser(RegisterModel model)
        {
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user != null)
            {
                // user already exists
                throw new Exception("Duplicated Email, please log in.");
            }
            var salt = GetRandomSalt();
            var hashedPassword = GetHashedPassword(model.Password, salt);
            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Salt = salt,
                DateOfBirth = model.DateOfBirth,
                HashedPassword = hashedPassword
            };
            await _userRepository.Add(newUser);
            return true;
        }

        public Task<bool> ValidateUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        private string GetRandomSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            return Convert.ToBase64String(salt);
        }

        private string GetHashedPassword(string password, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: Convert.FromBase64String(salt),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
