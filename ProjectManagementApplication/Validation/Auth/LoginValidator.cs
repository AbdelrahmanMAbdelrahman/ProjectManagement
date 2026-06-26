namespace ProjectManagementApplication.Validation.Auth
{
    public class LoginValidator:AbstractValidator<LoginReq>
    {
        public LoginValidator()
        {
            RuleFor(r => r.email)
                .NotEmpty().WithMessage("Must Provie Your Email")
                .Length(12, 40).WithMessage("Your Email Must Be within Range {MinLength} : {MaxLength} Character");
            RuleFor(r => r.password)
                .NotEmpty().WithMessage("Must Provie Your Password")
                .Length(8, 30).WithMessage("Your Password Must Be within Range {MinLength} : {MaxLength} Character");

        }
    }
}
