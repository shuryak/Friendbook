using AutoMapper;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FriendbookDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserRepository(FriendbookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public void Create(User user)
    {
        Entities.User userEntity = _mapper.Map<Entities.User>(user);

        _dbContext.Users.Add(userEntity);
        _dbContext.SaveChanges();
    }

    public IEnumerable<User> GetList(int offset, int limit)
    {
        List<User> userProfiles = _dbContext.Users
            .OrderBy(x => x.Id)
            .Skip(offset)
            .Take(limit)
            .Select(userProfile => _mapper.Map<User>(userProfile))
            .ToList();

        return userProfiles;
    }

    public User? GetById(int id)
    {
        Entities.User? userProfile = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        return _mapper.Map<User>(userProfile);
    }

    public User? GetByNickname(string nickname)
    {
        Entities.User? userProfile = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefault(x => x.Nickname == nickname);

        return _mapper.Map<User>(userProfile);
    }

    public IEnumerable<User> GetManyByIds(int[] ids)
    {
        List<User> userProfiles = _dbContext.Users
            .Where(x => ids.Contains(x.Id))
            .Select(userProfile => _mapper.Map<User>(userProfile))
            .ToList();

        return userProfiles;
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}
