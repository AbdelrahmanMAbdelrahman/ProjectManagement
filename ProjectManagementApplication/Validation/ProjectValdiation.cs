

using FluentValidation;
using ProjectManagementApplication.DTOs.Project;

namespace ProjectManagementApplication.Validation
{
    public class ProjectValidator:AbstractValidator<ProjectReq>
    {
        public ProjectValidator()
        {
            RuleFor(p => p.name)
                .NotEmpty().WithMessage("Must Provide Project name")
                .Length(3, 100).WithMessage("Project Title Must Be Withing Range {MinLength} : {MaxLength} Character");
            RuleFor(p => p.description)
                .MaximumLength(1000).WithMessage("Project Description Mustn't Exceeds 1000 Character");
            RuleFor(p => p.createdAt)
                .Must((date) => date <= DateTime.UtcNow.Date.AddDays(1))
                .WithMessage("Project Creation Date Mustn't Exceeds Now");
            
        }
    }
}
