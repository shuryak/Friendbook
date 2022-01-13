using AutoMapper;
using Friendbook.DataAccess.PostgreSql.Entities;

namespace Friendbook.DataAccess.PostgreSql;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<UserProfile, Entities.UserProfile>()
            .ReverseMap();
    }
}
