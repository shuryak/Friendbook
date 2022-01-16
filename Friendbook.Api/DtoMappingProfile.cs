using AutoMapper;
using Friendbook.Api.Models;
using Friendbook.Domain.Models;

namespace Friendbook.Api;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<CreateUserProfileDto, UserProfile>()
            .ForMember(x => x.DateOfBirth,
                s => s.MapFrom(x => new DateOnly(x.YearOfBirth, x.MonthOfBirth, x.DayOfBirth)))
            .ReverseMap();
    }
}
