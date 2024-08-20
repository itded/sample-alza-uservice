using Alza.UService.Infrastructure.DataAccess.Conventions;
using Alza.UService.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alza.UService.Infrastructure.DataAccess;

/// <summary>
/// The application's DB context.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Ctor.
    /// </summary>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // nothing
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<DboOrder>(entity =>
        {
            entity.ToTable("orders").HasIndex(u => u.OrderNumber).IsUnique();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasDefaultValueSql("newid()");
            entity.Property(e => e.OrderNumber);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.Property(e => e.OrderStatus).HasMaxLength(32).HasConversion<OrderStatusConvention>();
            entity.Property(e => e.CustomerName).HasMaxLength(32);
        });
        builder.Entity<DboOrderItem>(entity =>
        {
            entity.ToTable("order_items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasDefaultValueSql("newid()");
            entity.Property(e => e.ProductName).HasMaxLength(32);
            entity.Property(e => e.UnitPrice).HasPrecision(10, 4);
            entity.Property(e => e.Quantity);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
}
