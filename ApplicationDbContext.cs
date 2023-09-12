using Microsoft.EntityFrameworkCore;
using webapi_nextflow.Entity;

namespace webapi_nextflow
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<ApprovalType> ApprovalTypes { get; set; }

        public virtual DbSet<Conditional> Conditionals { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<GroupsUser> GroupsUsers { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<ParticipantType> ParticipantTypes { get; set; }

        public virtual DbSet<SpecificStatus> SpecificStatus { get; set; }

        public virtual DbSet<Stage> Stages { get; set; }

        public virtual DbSet<StatusFlowItem> StatusFlowItems { get; set; }

        public virtual DbSet<Entity.Task> Tasks { get; set; }

        public virtual DbSet<Transition> Transitions { get; set; }

        public virtual DbSet<Voting> Votings { get; set; }

        public virtual DbSet<WorkFlowItem> WorkFlowItems { get; set; }

        public virtual DbSet<Workflow> Workflows { get; set; }
        
        public virtual DbSet<Participant> Participants { get; set; }
        
        public virtual DbSet<Collaborator> Collaborators { get; set; }

        
    }
}

