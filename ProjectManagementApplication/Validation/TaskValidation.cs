

using FluentValidation;
using ProjectManagementApplication.DTOs.Task;

namespace ProjectManagementApplication.Validation
{
    public class TaskValidation:AbstractValidator<TaskReq>
    {
        public TaskValidation()
        {
            RuleFor(t => t.title).
                NotEmpty().WithMessage("Task Title Can't Be Empty")
                .Length(3, 100).WithMessage("Task Title Range is between {MinLength}:{MaxLength} Character");
            RuleFor(t => t.description)
                .MaximumLength( 1000).WithMessage("Task Mustn't Exceeds 1000 Character");
            RuleFor(t => t.status)
                .NotEmpty().WithMessage("Must Provide Task Status")
                .Length(3, 50).WithMessage("Task Status Range is between {MinLength}:{MaxLength} Character");
            RuleFor(t=>t.priority)
                .NotEmpty().WithMessage("Must Provide Task Priority");
            RuleFor(t => t.dueDate).Must((date)=>date>=DateTime.UtcNow.Date);
        }
    }
}
