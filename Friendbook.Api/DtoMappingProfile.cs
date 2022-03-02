using AutoMapper;
using Friendbook.Api.Models;
using Friendbook.Domain.Models;

namespace Friendbook.Api;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(x => x.DateOfBirth,
                s => s.MapFrom(x => new DateOnly(x.YearOfBirth, x.MonthOfBirth, x.DayOfBirth)));

        CreateMap<User, ShowUserDto>()
            .ForMember(x => x.DayOfBirth,
                s => s.MapFrom(x => x.DateOfBirth.Day))
            .ForMember(x => x.MonthOfBirth,
                s => s.MapFrom(x => x.DateOfBirth.Month))
            .ForMember(x => x.YearOfBirth,
                s => s.MapFrom(x => x.DateOfBirth.Year));
    }
}
