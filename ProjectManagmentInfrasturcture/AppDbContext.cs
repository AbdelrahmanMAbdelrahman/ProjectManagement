namespace ProjectManagmentInfrasturcture
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :
        IdentityDbContext<AppUser,AppRole,string>(options)
    {
        public DbSet<Project> Projects { get; set; } = default!;
        public DbSet<ProjectManagementDomain.Models.Task> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
