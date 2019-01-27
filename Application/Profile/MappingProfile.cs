using Application.Model;
using Domain.Entity;

namespace Application.Profile
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>();
        }
    }
}
