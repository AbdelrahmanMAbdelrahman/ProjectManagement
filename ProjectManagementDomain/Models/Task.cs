
namespace ProjectManagementDomain.Models
{
    public class Task
    {
     public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }=default!;
    }
}
