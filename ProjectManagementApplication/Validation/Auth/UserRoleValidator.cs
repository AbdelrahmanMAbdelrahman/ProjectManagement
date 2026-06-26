

using FluentValidation;
using ProjectManagementApplication.DTOs.Auth;

namespace ProjectManagementApplication.Validation.Auth
{
    public class UserRoleValidator:AbstractValidator<UserRoleReq>
    {
        public UserRoleValidator()
        {
            RuleFor(ur=>ur.email)
                .NotEmpty().WithMessage("Must Provide Email")
                .Length(12, 40).WithMessage("Your Email Must Be within Range {MinLength} : {MaxLength} Character");
            RuleFor(ur=>ur.role)
                .NotEmpty().WithMessage("Must Provide Role")
                .Length(3, 40).WithMessage("Role Must Be within Range {MinLength} : {MaxLength} Character"); ;
        }
    }
}
