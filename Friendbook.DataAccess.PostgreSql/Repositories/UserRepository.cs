using AutoMapper;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
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

        userEntity.CreatedAt = DateTime.UtcNow;
        
        _dbContext.Users.Add(userEntity);
        _dbContext.SaveChanges();
    }

    public IEnumerable<User> GetList(int offset, int limit)
    {
        List<User> users = _dbContext.Users
            .OrderBy(x => x.Id)
            .Skip(offset)
            .Take(limit)
            .Select(user => _mapper.Map<User>(user))
            .ToList();

        return users;
    }

    public User? GetById(int id)
    {
        Entities.User? user = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);

        return _mapper.Map<User>(user);
    }

    public User? GetByNickname(string nickname)
    {
        Entities.User? user = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefault(x => x.Nickname == nickname);

        return _mapper.Map<User>(user);
    }

    public IEnumerable<User> GetManyByIds(int[] ids)
    {
        List<User> users = _dbContext.Users
            .Where(x => ids.Contains(x.Id))
            .Select(user => _mapper.Map<User>(user))
            .ToList();

        return users;
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
