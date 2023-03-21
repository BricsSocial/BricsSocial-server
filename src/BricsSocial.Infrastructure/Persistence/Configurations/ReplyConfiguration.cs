using BricsSocial.Domain.Entities;
using BricsSocial.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Configurations
{
    public sealed class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.Property(r => r.AgentMessage)
                .HasMaxLength(Reply.Invariants.MessageMaxLength)
                .IsRequired(false);
            builder.Property(r => r.SpecialistMessage)
                .HasMaxLength(Reply.Invariants.MessageMaxLength)
                .IsRequired(false);
            builder.Property(r => r.ReplyStatus)
                .HasDefaultValue(ReplyStatus.Waiting);
        }
    }
}
