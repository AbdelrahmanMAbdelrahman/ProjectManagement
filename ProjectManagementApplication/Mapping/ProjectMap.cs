
namespace ProjectManagementApplication.Mapping
{
    public class ProjectMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProjectReq, Project>()
                .Map(des => des.Name, src => src.name)
                .Map(des => des.Description, src => src.description)
                .Map(des => des.CreatedAt, src => src.createdAt);

            config.NewConfig<Project, ProjectRes>()
                .Map(des => des.id, src => src.Id)
                .Map(des => des.name, src => src.Name)
                .Map(des => des.description, src => src.Description)
                .Map(des => des.createdAt, src => src.CreatedAt);
        }
    }
}
