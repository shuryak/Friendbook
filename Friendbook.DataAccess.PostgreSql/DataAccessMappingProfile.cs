using AutoMapper;
using Friendbook.Domain.Models;

namespace Friendbook.DataAccess.PostgreSql;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<UserProfile, Entities.UserProfile>()
            .ReverseMap();

        CreateMap<Entities.Chats.Chat, Chat>()
            .ForMember(x => x.CreatedAt,
                s => s.MapFrom(x => x.CreatedAt == x.CreatedAt.ToUniversalTime()))
            .ReverseMap();
    }
}
