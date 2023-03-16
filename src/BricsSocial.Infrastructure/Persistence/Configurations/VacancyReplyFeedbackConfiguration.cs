using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Configurations
{
    public sealed class VacancyReplyFeedbackConfiguration : IEntityTypeConfiguration<VacancyReplyFeedback>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VacancyReplyFeedback> builder)
        {
            builder.Property(r => r.Message)
                 .HasMaxLength(VacancyReplyFeedback.Invariants.MessageMaxLength)
                 .IsRequired();
        }
    }
}
