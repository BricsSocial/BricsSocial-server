using BricsSocial.Application.Common.Models;
using BricsSocial.Domain.Entities;
using BricsSocial.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Configurations
{
    public sealed class SpecialistConfiguration : IEntityTypeConfiguration<Specialist>
    {
        public void Configure(EntityTypeBuilder<Specialist> builder)
        {
            builder.HasOne<ApplicationUser>().WithOne().HasForeignKey<Specialist>(s => s.IdentityId);
            builder.Property(s => s.IdentityId)
                .IsRequired();
            builder.Property(s => s.Email)
                .IsRequired();
            builder.Property(s => s.FirstName)
                .HasMaxLength(Specialist.Invariants.FirstNameMaxLength)
                .IsRequired();
            builder.Property(s => s.LastName)
                .HasMaxLength(Specialist.Invariants.LastNameMaxLength)
                .IsRequired();

            builder.Property(s => s.Bio)
                .HasMaxLength(Specialist.Invariants.BioMaxLength)
                .HasDefaultValue(string.Empty)
                .IsRequired();
            builder.Property(r => r.Skills)
                .HasMaxLength(Specialist.Invariants.SkillsMaxLength)
                .HasDefaultValue(string.Empty)
                .IsRequired();
            builder.Property(r => r.Experience)
                .HasMaxLength(Specialist.Invariants.ExperienceMaxLength)
                .HasDefaultValue(string.Empty)
                .IsRequired();

            builder.Property(c => c.Photo)
                .HasMaxLength(Specialist.Invariants.PhotoMaxLength)
                .IsRequired(false);
            
            builder.HasMany(r => r.SkillTags).WithMany(s => s.Specialists);
        }
    }
}
