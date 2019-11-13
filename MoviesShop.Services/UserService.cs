using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entities;
using MovieShop.Data;

namespace MovieShop.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptoService _cryptoService;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
        }
        public async Task<User> CreateUser(string email, string password, string firstName, string lastName)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
                if (dbUser!=null)
                {
                return null;
                }
            var salt = _cryptoService.CreateSalt();
            var hashcode = _cryptoService.HashPassword(password, salt);

            var user = new User
            {
                Email = email,
                HashedPassword = hashcode,
                FirstName = firstName,
                LastName = lastName,
                Salt=salt
            };
           
            return await _userRepository.AddAsync(user);
        }

        public async Task<IEnumerable<Purchase>> GetPurchases( int userId)
        {
            return await _userRepository.GetUserPurchasedMovie(userId);
        }

        public async Task<User> VaildateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user==null)
            {
                return null;
            }
            var salt = user.Salt;
            var hashpassword = user.HashedPassword;
            var newPasswaord = _cryptoService.HashPassword(password, salt);
            if (hashpassword == newPasswaord)
            {

                return user;
            }
            else
            {
                return null;
            }




        }
    }
}
