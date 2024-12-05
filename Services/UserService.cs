using BCrypt.Net;
using ChocolateFactory.Models;
using ChocolateFactory.Repositories;
using ChocolateFactory.Helpers;

namespace ChocolateFactory.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<User>> GetUsersByUserRoleAsync(UserRole role) {
            return await _userRepository.GetUserByUserRoleAsync(role);
        }
    }
}
