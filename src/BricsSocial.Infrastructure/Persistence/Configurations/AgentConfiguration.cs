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
    public sealed class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasOne<ApplicationUser>().WithOne().HasForeignKey<Agent>(a => a.IdentityId);
            builder.Property(a => a.IdentityId)
                .IsRequired();
            builder.Property(a => a.Email)
                .IsRequired();
            builder.Property(a => a.FirstName)
                .HasMaxLength(Agent.Invariants.FirstNameMaxLength)
                .IsRequired();
            builder.Property(a => a.LastName)
                .HasMaxLength(Agent.Invariants.LastNameMaxLength)
                .IsRequired();

            builder.Property(a => a.Position)
                .HasMaxLength(Agent.Invariants.PositionMaxLength)
                .IsRequired();
        }
    }
}
