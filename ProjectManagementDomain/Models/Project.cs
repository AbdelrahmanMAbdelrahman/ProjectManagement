
namespace ProjectManagementDomain.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public ICollection<Task> Tasks { get; set; } = default!;   
    }
}
