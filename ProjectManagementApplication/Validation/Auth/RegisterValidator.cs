
using FluentValidation;
using ProjectManagementApplication.DTOs.Auth;

namespace ProjectManagementApplication.Validation.Auth
{
    public class RegisterValidator:AbstractValidator<RegisterReq>
    {
        public RegisterValidator()
        {
            RuleFor(r=>r.name)
                .NotEmpty().WithMessage("Must Provie Your Name")
                .Length(3,100).WithMessage("Your Name Must Be within Range {MinLength} : {MaxLength} Character");
            RuleFor(r=>r.email)
                .NotEmpty().WithMessage("Must Provie Your Eail")
                .Length(12,40).WithMessage("Your Email Must Be within Range {MinLength} : {MaxLength} Character");
            RuleFor(r=>r.password)
                .NotEmpty().WithMessage("Must Provie Your Password")
                .Length(8,30).WithMessage("Your Password Must Be within Range {MinLength} : {MaxLength} Character");
            RuleFor(r=>r.userName)
                .NotEmpty().WithMessage("Must Provie Your User Name")
                .Length(12, 40).WithMessage("Your User Name Must Be within Range {MinLength} : {MaxLength} Character");
            RuleFor(r=>r.phone)
                .NotEmpty().WithMessage("Must Provie Your phone")
                .Length(11,14).WithMessage("Your phone Must Be within Range {MinLength} : {MaxLength} Character");
        }
    }
}
 