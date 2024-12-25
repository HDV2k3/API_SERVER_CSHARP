using lab1_api.Models.Domain;
using lab1_api.Models.DTOs;

namespace lab1_api.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> AddUserAsync(UserRequestCreated user);
        Task<UserDto> UpdateUserAsync(int id, UserRequest user);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDto> FindUserByEmailAsync(string email);

    }
}
