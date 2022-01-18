using AutoMapper;
using Friendbook.Domain;
using Microsoft.EntityFrameworkCore;
using UserProfile = Friendbook.Domain.Models.UserProfile;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly FriendbookDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserProfileRepository(FriendbookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public void Create(UserProfile userProfile)
    {
        Entities.UserProfile userProfileEntity = _mapper.Map<Entities.UserProfile>(userProfile);

        _dbContext.UserProfiles.Add(userProfileEntity);
        _dbContext.SaveChanges();
    }

    public IEnumerable<UserProfile> GetList(int offset, int limit)
    {
        List<UserProfile> userProfiles = _dbContext.UserProfiles
            .Skip(offset)
            .Take(limit)
            .Select(userProfile => _mapper.Map<UserProfile>(userProfile))
            .ToList();

        return userProfiles;
    }

    public UserProfile GetById(int id)
    {
        Entities.UserProfile? userProfile = _dbContext.UserProfiles
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        return _mapper.Map<UserProfile>(userProfile);
    }

    public UserProfile GetByNickname(string nickname)
    {
        Entities.UserProfile? userProfile = _dbContext.UserProfiles
            .AsNoTracking()
            .FirstOrDefault(x => x.Nickname == nickname);

        return _mapper.Map<UserProfile>(userProfile);
    }

    public IEnumerable<UserProfile> GetManyByIds(int[] ids)
    {
        List<UserProfile> userProfiles = _dbContext.UserProfiles
            .Where(x => ids.Contains(x.Id))
            .Select(userProfile => _mapper.Map<UserProfile>(userProfile))
            .ToList();

        return userProfiles;
    }

    public void Update(UserProfile userProfile)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}
