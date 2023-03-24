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
            builder.Property(r => r.Status)
                .HasDefaultValue(ReplyStatus.Pending);
            builder.HasOne(r => r.Specialist).WithMany(s => s.Replies).HasForeignKey(r => r.SpecialistId);
            builder.HasOne(r => r.Vacancy).WithMany(v => v.Replies).HasForeignKey(r => r.VacancyId);
        }
    }
}
