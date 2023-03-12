﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BricsSocial.Domain.Entities;

namespace BricsSocial.Infrastructure.Persistence.Configurations
{
    public sealed class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.Property(v => v.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(t => t.Requirements)
                .HasMaxLength(1500)
                .IsRequired();
            builder.Property(t => t.Offerings)
                .HasMaxLength(1500)
                .IsRequired();
        }
    }
}