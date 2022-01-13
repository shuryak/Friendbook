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
        modelBuilder.Entity<UserProfile>()
            .HasData(new[] 
            {
                new UserProfile
                {
                    Id = 1,
                    Nickname = "shuryak",
                    FirstName = "Alexander",
                    LastName = "Konovalov",
                    DateOfBirth = new DateOnly(2004, 1, 24)
                }
            });

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
}
