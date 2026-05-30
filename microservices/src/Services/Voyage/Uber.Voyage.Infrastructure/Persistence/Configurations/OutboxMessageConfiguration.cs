using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Uber.Voyage.Infrastructure.Persistence.Outbox;

namespace Uber.Voyage.Infrastructure.Persistence.Configurations;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Payload)
            .IsRequired()
            .HasColumnType("jsonb");

        builder.Property(x => x.OccurredAt)
            .IsRequired();

        builder.Property(x => x.ProcessedAt)
            .IsRequired(false);

        builder.Property(x => x.Error)
            .IsRequired(false)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.ProcessedAt);
    }
}