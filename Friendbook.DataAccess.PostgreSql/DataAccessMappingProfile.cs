using AutoMapper;
using Friendbook.Domain.Models;

namespace Friendbook.DataAccess.PostgreSql;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<User, Entities.User>()
            .ReverseMap();

        CreateMap<UserSession, Entities.UserSession>()
            .ForMember(x => x.Id,
                s => s.MapFrom(x => x.SessionId))
            .ReverseMap();

        CreateMap<Entities.Chats.Chat, Chat>()
            .ReverseMap();
    }
}
