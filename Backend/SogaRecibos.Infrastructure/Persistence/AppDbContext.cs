using Microsoft.EntityFrameworkCore;
using SogaRecibos.Domain.Receipts;
using SogaRecibos.Domain.Users;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SogaRecibos.Infrastructure.Persistence;
public sealed class AppDbContext : DbContext
{
    public DbSet<Receipt> Receipts => Set<Receipt>();
    public DbSet<User> Users => Set<User>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder b) => b.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
