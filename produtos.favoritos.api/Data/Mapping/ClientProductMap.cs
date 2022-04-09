using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ClientProductMap : IEntityTypeConfiguration<ClientProductEntity>
    {
        public void Configure(EntityTypeBuilder<ClientProductEntity> builder)
        {
            builder.ToTable("ClientProduct");
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Id).IsUnique();
            builder.Property(u => u.IdClient).IsRequired().HasMaxLength(60);
            builder.Property(u => u.IdProduct).IsRequired().HasMaxLength(60);
        }
    }
}
