using FluentValidation;
using FluentValidation.Results;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;

namespace Friendbook.BusinessLogic;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;

    public UserService(IUserRepository userRepository, IValidator<User> userValidator)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
    }

    public bool Create(User user)
    {
        bool isValid = Validate(user);
        
        if (isValid) _userRepository.Create(user);

        return isValid;
    }

    public User? GetById(int id)
    {
        return _userRepository.GetById(id);
    }

    public User? GetByNickname(string nickname)
    {
        return _userRepository.GetByNickname(nickname);
    }

    public IEnumerable<User> GetList(int offset = 0, int limit = 10)
    {
        return _userRepository.GetList(offset, limit);
    }

    private bool Validate(User user)
    {
        ValidationResult result = _userValidator.Validate(user);

        return result.IsValid;
    }
}
