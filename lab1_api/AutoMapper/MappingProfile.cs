using AutoMapper;
using lab1_api.Models.Domain;
using lab1_api.Models.DTOs;

namespace lab1_api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequestCreated, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
