using Microsoft.EntityFrameworkCore;
using SmsSender.BillingService.Domain.Entities;

namespace SmsSender.BillingService.Domain;

public partial class BillingDbContext : DbContext
{
    public BillingDbContext()
    {
    }

    public BillingDbContext(DbContextOptions<BillingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SmsProfile> SmsProfiles { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SmsProfile>(entity =>
        {
            entity.Property(e => e.SmsProfileId).UseIdentityAlwaysColumn();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
