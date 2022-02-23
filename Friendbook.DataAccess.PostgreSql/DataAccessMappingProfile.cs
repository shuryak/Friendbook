using AutoMapper;
using Friendbook.Domain.Models;

namespace Friendbook.DataAccess.PostgreSql;

public class DataAccessMappingProfile : Profile
{
    public DataAccessMappingProfile()
    {
        CreateMap<UserProfile, Entities.UserProfile>()
            .ReverseMap();

        CreateMap<Chat, Entities.Chats.Chat>()
            .ReverseMap();
    }
}
