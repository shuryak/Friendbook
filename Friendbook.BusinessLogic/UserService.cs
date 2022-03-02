using FluentValidation;
using FluentValidation.Results;
using Friendbook.Domain;
using Friendbook.Domain.Models;

namespace Friendbook.BusinessLogic;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userProfileValidator;

    public UserService(IUserRepository userRepository, IValidator<User> userProfileValidator)
    {
        _userRepository = userRepository;
        _userProfileValidator = userProfileValidator;
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
        ValidationResult result = _userProfileValidator.Validate(user);

        return result.IsValid;
    }
}
