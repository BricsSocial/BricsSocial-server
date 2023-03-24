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
    public sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        private static List<Country> _countries = new List<Country>
            {
                new Country { Name = "Brasil" },
                new Country { Name = "Russia" },
                new Country { Name = "India" },
                new Country { Name = "China" },
                new Country { Name = "South Africa" },
            };

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(Country.Invariants.NameMaxLength)
                .IsRequired();


            //builder.HasData(_countries);

        }
    }
}
