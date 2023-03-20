using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Configurations
{
    public sealed class ResumeConfiguration : IEntityTypeConfiguration<Resume>
    {
        public void Configure(EntityTypeBuilder<Resume> builder)
        {
            builder.Property(r => r.Skills)
                .HasMaxLength(Resume.Invariants.SkillsMaxLength)
                .IsRequired();
            builder.Property(r => r.Experience)
                .HasMaxLength(Resume.Invariants.ExperienceMaxLength)
                .IsRequired();
            builder.HasMany(r => r.SkillTags).WithMany(s => s.Resumes);
        }
    }
}
