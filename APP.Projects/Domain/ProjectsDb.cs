using Microsoft.EntityFrameworkCore;

namespace APP.Projects.Domain
{
    public class ProjectsDb : DbContext
    {
        public ProjectsDb(DbContextOptions<ProjectsDb> options) : base(options)
        {
        }

        public DbSet<BookTag> BookTags { get; set; }
    }
}