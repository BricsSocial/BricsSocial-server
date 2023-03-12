using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BricsSocial.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    //DbSet<Specialist> Specialists { get; }
    DbSet<Agent> Agents { get; }
    DbSet<Country> Countries { get; }
    DbSet<Company> Companies { get; }

    DbSet<Vacancy> Vacancies { get; }
    //DbSet<VacancyReply> VacancyReplies { get; }
    //DbSet<VacancyReplyFeedBack> VacancyReplyFeedBacks { get; }

    //DbSet<Resume> Resumes { get; }
    //DbSet<ResumeReply> ResumeReplies { get; }
    //DbSet<ResumeReplyFeedback> ResumeReplyFeedbacks { get; }

    //DbSet<FriendRequest> FriendRequests { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
