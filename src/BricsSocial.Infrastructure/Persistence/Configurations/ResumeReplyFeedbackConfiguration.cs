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
    public sealed class ResumeReplyFeedbackConfiguration : IEntityTypeConfiguration<ResumeReplyFeedback>
    {
        public void Configure(EntityTypeBuilder<ResumeReplyFeedback> builder)
        {
            builder.Property(r => r.Message)
                 .HasMaxLength(ResumeReplyFeedback.Invariants.MessageMaxLength)
                 .IsRequired();
        }
    }
}
