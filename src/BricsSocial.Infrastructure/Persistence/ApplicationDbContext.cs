using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Domain.Entities;
using BricsSocial.Infrastructure.Identity;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
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
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
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
