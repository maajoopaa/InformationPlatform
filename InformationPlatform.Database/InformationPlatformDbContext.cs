using InformationPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationPlatform.Database;

public class InformationPlatformDbContext : DbContext
{
    public InformationPlatformDbContext(DbContextOptions<InformationPlatformDbContext> options)
        : base(options) { }
    
    public DbSet<DbChat> Chats { get; set; }
    
    public DbSet<DbComment> Comment { get; set; }
    
    public DbSet<DbImage> Images { get; set; }
    
    public DbSet<DbLike> Likes { get; set; }
    
    public DbSet<DbMessage> Messages { get; set; }
    
    public DbSet<DbPost> Posts { get; set; }
    
    public DbSet<DbUser> Users { get; set; }
    
    public DbSet<DbUserSettings> UserSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbUser>()
            .HasIndex(x => x.Username)
            .IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
}