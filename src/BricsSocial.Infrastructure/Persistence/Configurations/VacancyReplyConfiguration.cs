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
    public sealed class VacancyReplyConfiguration : IEntityTypeConfiguration<VacancyReply>
    {
        public void Configure(EntityTypeBuilder<VacancyReply> builder)
        {
            builder.Property(v => v.Message)
               .HasMaxLength(VacancyReply.Invariants.MessageMaxLength)
               .IsRequired();
        }
    }
}
