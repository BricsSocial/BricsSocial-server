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
    public sealed class ResumeReplyConfiguration : IEntityTypeConfiguration<ResumeReply>
    {
        public void Configure(EntityTypeBuilder<ResumeReply> builder)
        {
            builder.Property(r => r.Message)
                 .HasMaxLength(ResumeReply.Invariants.MessageMaxLength)
                 .IsRequired();
        }
    }
}
