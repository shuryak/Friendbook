using Microsoft.EntityFrameworkCore;
using Friendbook.DataAccess.PostgreSql.Entities;

namespace Friendbook.DataAccess.PostgreSql;

public class FriendbookDbContext : DbContext
{
    public FriendbookDbContext(DbContextOptions<FriendbookDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasAlternateKey(u => u.Nickname);

        modelBuilder.Entity<UserSession>()
            .HasIndex(us => us.RefreshToken)
            .IsUnique();
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<UserSession> UserSessions { get; set; }

    public DbSet<FollowerPair> FollowerPairs { get; set; }
}
