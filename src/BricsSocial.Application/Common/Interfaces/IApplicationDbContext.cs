using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BricsSocial.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Country> Countries { get; }
    DbSet<Specialist> Specialists { get; }
    DbSet<Agent> Agents { get; }
    DbSet<Company> Companies { get; }

    DbSet<Vacancy> Vacancies { get; }
    DbSet<Reply> Replies { get; }

    DbSet<SkillTag> SkillTags { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
