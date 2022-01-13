using AutoMapper;
using Friendbook.Domain;
using Microsoft.EntityFrameworkCore;
using UserProfile = Friendbook.Domain.Models.UserProfile;

namespace Friendbook.DataAccess.PostgreSql.Repository;

public class UserProfileRepository : IRepository<UserProfile>
{
    private readonly IMapper _mapper;
    private readonly FriendbookDbContext _dbContext;

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
        List<Entities.UserProfile> userProfiles = _dbContext.UserProfiles
            .AsNoTracking().ToList();

        return userProfiles as IEnumerable<UserProfile>;
    }

    public UserProfile Get(int id)
    {
        Entities.UserProfile? userProfile = _dbContext.UserProfiles
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        return _mapper.Map<UserProfile>(userProfile);
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
