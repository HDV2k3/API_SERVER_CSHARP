using lab1_api.Interfaces;
using lab1_api.Models.Domain;
using lab1_api.Models.DTOs;

namespace lab1_api.Facade
{
    public class UserFacade 
    {
        private readonly IUserService _userService;

        public UserFacade(IUserService userService)
        {
            _userService = userService; 
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _userService.GetAllUsersAsync();
        }
        public async Task<UserDto> AddUserAsync(UserRequestCreated user)
        {
           return await _userService.AddUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userService.DeleteUserAsync(id);
        }

        public async Task<UserDto> FindUserByEmailAsync(string email)
        {
            return await _userService.FindUserByEmailAsync(email);
        }

 

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UserRequest user)
        {
           return await _userService.UpdateUserAsync(id, user);
        }
    }
}
