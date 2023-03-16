using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Domain.Entities;
using BricsSocial.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BricsSocial.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Specialist> Specialists => Set<Specialist>();
        public DbSet<Agent> Agents => Set<Agent>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Vacancy> Vacancies => Set<Vacancy>();
        public DbSet<VacancyReply> VacancyReplies => Set<VacancyReply>();
        public DbSet<VacancyReplyFeedback> VacancyReplyFeedbacks => Set<VacancyReplyFeedback>();
        public DbSet<Resume> Resumes => Set<Resume>();
        public DbSet<ResumeReply> ResumeReplies => Set<ResumeReply>();
        public DbSet<ResumeReplyFeedback> ResumeReplyFeedbacks => Set<ResumeReplyFeedback>();
        public DbSet<FriendRequest> FriendRequests => Set<FriendRequest>();

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
