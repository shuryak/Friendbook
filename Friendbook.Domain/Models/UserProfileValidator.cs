using FluentValidation;

namespace Friendbook.Domain.Models;

public class UserProfileValidator : AbstractValidator<UserProfile>
{
    public UserProfileValidator()
    {
        RuleFor(p => p.Nickname)
            .Must(x => x.All(char.IsLetterOrDigit))
            .MinimumLength(6)
            .MaximumLength(16);

        RuleFor(p => p.FirstName)
            .Must(x => x.All(char.IsLetter))
            .MinimumLength(2)
            .MaximumLength(32);
        
        RuleFor(p => p.LastName)
            .Must(x => x.All(char.IsLetter))
            .MinimumLength(2)
            .MaximumLength(32);

        RuleFor(p => p.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-14)));

        RuleFor(p => p.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(99)));
    }
}
