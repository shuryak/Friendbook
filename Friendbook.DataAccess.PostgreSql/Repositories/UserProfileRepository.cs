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

    public IEnumerable<UserProfile> GetList()
    {
        List<UserProfile> userProfiles = _dbContext.UserProfiles
            .AsNoTracking()
            .ToList()
            .Select(userProfile => _mapper.Map<UserProfile>(userProfile))
            .ToList();

        return userProfiles;
    }

    public IEnumerable<UserProfile> GetMany(int[] ids)
    {
        throw new NotImplementedException();
    }

    public UserProfile Get(int id)
    {
        Entities.UserProfile? userProfile = _dbContext.UserProfiles
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        return _mapper.Map<UserProfile>(userProfile);
    }

    public IEnumerable<UserProfile> GetManyByIds(int[] ids)
    {
        List<UserProfile> userProfiles = _dbContext.UserProfiles
            .Where(x => ids.Contains(x.Id))
            .ToList()
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
