using FluentValidation;
using FluentValidation.Results;
using Friendbook.Domain;
using Friendbook.Domain.Models;

namespace Friendbook.BusinessLogic;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserProfile> _userProfileValidator;

    public UserService(IUserRepository userRepository, IValidator<UserProfile> userProfileValidator)
    {
        _userRepository = userRepository;
        _userProfileValidator = userProfileValidator;
    }

    public bool Create(UserProfile userProfile)
    {
        bool isValid = Validate(userProfile);
        
        if (isValid) _userRepository.Create(userProfile);

        return isValid;
    }

    public UserProfile GetById(int id)
    {
        return _userRepository.GetById(id);
    }

    public UserProfile GetByNickname(string nickname)
    {
        return _userRepository.GetByNickname(nickname);
    }

    public IEnumerable<UserProfile> GetList(int offset = 0, int limit = 10)
    {
        return _userRepository.GetList(offset, limit);
    }

    private bool Validate(UserProfile userProfile)
    {
        ValidationResult result = _userProfileValidator.Validate(userProfile);

        return result.IsValid;
    }
}
