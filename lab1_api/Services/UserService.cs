using AutoMapper;
using lab1_api.Models;
using lab1_api.Models.Domain;
using lab1_api.Interfaces;
using lab1_api.Exceptions;
using lab1_api.Models.DTOs;

namespace lab1_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
            catch
            {
                throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    throw new AppException(ErrorCode.USER_NOT_EXISTED);
                }
                return _mapper.Map<UserDto>(user);
            }
            catch
            {
                throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
            }
        }

        public async Task<UserDto> AddUserAsync(UserRequestCreated user)
        {
            try
            {
                // Kiểm tra email
                if (string.IsNullOrEmpty(user.Email))
                {
                    throw new AppException(ErrorCode.INVALID_EMAIL);
                }

                var existingUser = await _userRepository.FindUserByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    throw new AppException(ErrorCode.USER_ALREADY_EXISTED);
                }

                // Sử dụng AutoMapper để chuyển từ DTO sang Domain Model
                var newUser = _mapper.Map<User>(user);  // Sử dụng AutoMapper để chuyển đổi từ UserRequestCreated sang User

                // Tạo người dùng mới và lưu vào database
                var createdUser = await _userRepository.CreateUserAsync(newUser);

                // Chuyển đối tượng User sang UserDto và trả về
                return _mapper.Map<UserDto>(createdUser);
            }
            catch
            {
                throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
            }
        }


        public async Task<UserDto> UpdateUserAsync(int id, UserRequest user)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(id);
                if (existingUser == null)
                {
                    throw new AppException(ErrorCode.USER_NOT_EXISTED);
                }

                existingUser.Name = user.Name;
                existingUser.Password = user.Password;
                existingUser.Avatar = user.Avatar;

                var updatedUser = await _userRepository.UpdateUserAsync(existingUser);
                return _mapper.Map<UserDto>(updatedUser);
            }
            catch
            {
                throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    throw new AppException(ErrorCode.USER_NOT_EXISTED);
                }

                return await _userRepository.DeleteUserAsync(id);
            }
            catch
            {
                throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
            }
        }

        public async Task<UserDto> FindUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.FindUserByEmailAsync(email);
                if (user == null)
                {
                    throw new AppException(ErrorCode.USER_NOT_EXISTED);
                }
                return _mapper.Map<UserDto>(user);
            }
            catch
            {
                throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
            }
        }
    }
}
