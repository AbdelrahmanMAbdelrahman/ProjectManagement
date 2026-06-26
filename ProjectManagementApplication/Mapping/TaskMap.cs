
namespace ProjectManagementApplication.Mapping
{
    public class TaskMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TaskReq, ProjectManagementDomain.Models.Task>()
                .Map(des => des.Title, src => src.title)
                .Map(des => des.Description, src => src.description)
                .Map(des => des.DueDate, src => src.dueDate)
                .Map(des => des.Priority, src => src.priority)
                .Map(des => des.ProjectId, src => src.projectId)
                .Map(des => des.Status, src => src.status);


            config.NewConfig<ProjectManagementDomain.Models.Task, TaskRes >()
                .Map(des => des.id, src => src.Id)
                .Map(des => des.title, src => src.Title)
                .Map(des => des.description, src => src.Description)
                .Map(des => des.dueDate, src => src.DueDate)
                .Map(des => des.priority, src => src.Priority)
                .Map(des => des.projectId, src => src.ProjectId)
                .Map(des => des.status, src => src.Status);
        }
    }
}
