using AutoMapper;
using Awktion.API.Models;

namespace Awktion.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterForm, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(r => r.Name));
    }
}
