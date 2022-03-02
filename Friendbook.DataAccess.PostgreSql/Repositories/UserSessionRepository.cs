using AutoMapper;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class UserSessionRepository : IUserSessionRepository
{
    private readonly FriendbookDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public UserSessionRepository(FriendbookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public UserSession Create(User user, TimeSpan expiresIn)
    {
        Entities.UserSession userSessionEntity = new Entities.UserSession
        {
            UserId = user.Id,
            RefreshToken = TokenGenerator.GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow + expiresIn
        };
        
        EntityEntry<Entities.UserSession> userSession = _dbContext.UserSessions.Add(userSessionEntity);

        _dbContext.SaveChanges();

        return _mapper.Map<UserSession>(userSessionEntity);
    }

    public UserSession GetById(int sessionId)
    {
        Entities.UserSession? userSessionEntity = _dbContext.UserSessions.FirstOrDefault(us => us.Id == sessionId);

        return _mapper.Map<UserSession>(userSessionEntity);
    }

    public UserSession Update(UserSession userSession, TimeSpan expiresIn)
    {
        Entities.UserSession? userSessionEntity = _mapper.Map<Entities.UserSession>(userSession);
        
        userSessionEntity.ExpiresAt = DateTime.Now + expiresIn;
        userSessionEntity.RefreshToken = TokenGenerator.GenerateRefreshToken();
        
        EntityEntry<Entities.UserSession> userSessionUpdatedEntity = _dbContext.UserSessions.Update(userSessionEntity);
        _dbContext.SaveChanges();
        
        return _mapper.Map<UserSession>(userSessionUpdatedEntity);
    }
}
