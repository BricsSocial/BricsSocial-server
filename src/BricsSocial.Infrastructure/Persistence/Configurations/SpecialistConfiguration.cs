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
            
            builder.Property(s => s.ShortBio)
                .HasMaxLength(Specialist.Invariants.ShortBioMaxLength);
            builder.Property(s => s.LongBio)
                .HasMaxLength(Specialist.Invariants.LongBioMaxLength);
            builder.Property(c => c.Photo)
                .HasMaxLength(100_000);

            builder.HasOne(s => s.Resume).WithOne(r => r.Specialist).HasForeignKey<Resume>(r => r.SpecialistId);
            builder.HasMany(s => s.FromFriendRequests).WithOne(f => f.FromSpecialist);
            builder.HasMany(s => s.ToFriendRequests).WithOne(f => f.ToSpecialist);
        }
    }
}
