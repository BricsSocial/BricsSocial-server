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
    public sealed class SkillTagConfiguration : IEntityTypeConfiguration<SkillTag>
    {
        public void Configure(EntityTypeBuilder<SkillTag> builder)
        {
            builder.HasIndex(s => s.Name)
                .IsUnique();
            builder.Property(s => s.Name)
               .HasMaxLength(SkillTag.Invariants.NameMaxLength)
               .IsRequired();
        }
    }
}
