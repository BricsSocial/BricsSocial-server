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
            builder.HasOne<ApplicationUser>().WithOne().HasForeignKey<Specialist>(a => a.IdentityId);
            builder.HasOne(s => s.Resume).WithOne(r => r.Specialist).HasForeignKey<Resume>(r => r.SpecialistId);
            builder.HasMany(s => s.FromFriendRequests).WithOne(f => f.FromSpecialist);//.HasForeignKey(f => f.FromSpecialistId);
            builder.HasMany(s => s.ToFriendRequests).WithOne(f => f.ToSpecialist);//.HasForeignKey(f => f.ToSpecialistId);
            builder.Property(s => s.IdentityId)
                .IsRequired();
            builder.Property(s => s.About)
                .HasMaxLength(1500);
            builder.Property(c => c.Photo)
                .HasMaxLength(100_000);
        }
    }
}
