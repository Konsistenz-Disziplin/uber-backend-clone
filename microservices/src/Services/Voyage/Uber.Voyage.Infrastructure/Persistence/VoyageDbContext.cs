using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Uber.Voyage.Domain.Entities;
using Uber.Voyage.Infrastructure.Persistence.Outbox;

namespace Uber.Voyage.Infrastructure.Persistence;

public sealed class VoyageDbContext(DbContextOptions<VoyageDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.Voyage> Voyages => Set <Domain.Entities.Voyage >();
    public DbSet<OutboxMessage> Messages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.ApplyConfigurationsFromAssembly(typeof(VoyageDbContext).Assembly);
}
