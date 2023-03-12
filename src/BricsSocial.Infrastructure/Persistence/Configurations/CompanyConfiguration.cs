﻿using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure.Persistence.Configurations
{
    public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.Description)
                .HasMaxLength(3000)
                .IsRequired();
            builder.Property(c => c.Logo)
                .HasMaxLength(100_000);
        }
    }
}
