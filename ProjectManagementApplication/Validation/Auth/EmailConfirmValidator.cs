
using FluentValidation;
using ProjectManagementApplication.DTOs.Auth;

namespace ProjectManagementApplication.Validation.Auth
{
    public class EmailConfirmValidator:AbstractValidator<EmailConfirmReq>
    {
        public EmailConfirmValidator()
        {
            
        RuleFor(r=>r.code)
                .NotEmpty().WithMessage("Must Provie Your code")
                .MinimumLength(10).WithMessage("Your code Must Be 10 or more Character");
        RuleFor(r=>r.id)
                .NotEmpty().WithMessage("Must Provie Id")
                .MinimumLength(10).WithMessage("Your Password Must Be 10 or more Character");

    }
        }
}
