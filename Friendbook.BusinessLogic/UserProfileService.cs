using FluentValidation;
using FluentValidation.Results;
using Friendbook.Domain;
using Friendbook.Domain.Models;

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

    public UserProfile Get(int id)
    {
        throw new NotImplementedException();
    }

    private bool Validate(UserProfile userProfile)
    {
        ValidationResult result = _userProfileValidator.Validate(userProfile);

        return result.IsValid;
    }
}
