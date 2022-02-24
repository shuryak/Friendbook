using FluentValidation;
using FluentValidation.Results;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;

namespace Friendbook.BusinessLogic;

public class UserProfileService : IUserProfileService
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IValidator<UserProfile> _userProfileValidator;

    public UserProfileService(IUserProfileRepository userProfileRepository, IValidator<UserProfile> userProfileValidator)
    {
        _userProfileRepository = userProfileRepository;
        _userProfileValidator = userProfileValidator;
    }

    public bool Create(UserProfile userProfile)
    {
        bool isValid = Validate(userProfile);
        
        if (isValid) _userProfileRepository.Create(userProfile);

        return isValid;
    }

    public UserProfile GetById(int id)
    {
        return _userProfileRepository.GetById(id);
    }

    public UserProfile GetByNickname(string nickname)
    {
        return _userProfileRepository.GetByNickname(nickname);
    }

    public IEnumerable<UserProfile> GetList(int offset = 0, int limit = 10)
    {
        return _userProfileRepository.GetList(offset, limit);
    }

    private bool Validate(UserProfile userProfile)
    {
        ValidationResult result = _userProfileValidator.Validate(userProfile);

        return result.IsValid;
    }
}
