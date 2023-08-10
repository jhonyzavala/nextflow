using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Entity.Task> Tasks { get; set; }
        public DbSet<Conditional> Conditional { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupsUser> GroupsUser { get; set; }
        public DbSet<SpecificStatus> SpecificStatus { get; set; }
        public DbSet<Voting> Votings { get; set; }
        
        
    }
}

