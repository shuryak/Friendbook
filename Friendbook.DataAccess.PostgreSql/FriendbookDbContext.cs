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
                    Nickname = "shuryak",
                    FirstName = "Alexander",
                    LastName = "Konovalov",
                    DateOfBirth = new DateOnly(2004, 1, 24)
                },
                new UserProfile
                {
                    Nickname = "ivan03vdovin",
                    FirstName = "Ivan",
                    LastName = "Vdovin",
                    DateOfBirth = new DateOnly(2003, 2, 16)
                },
                new UserProfile
                {
                    Nickname = "archii",
                    FirstName = "Artur",
                    LastName = "Ivanov",
                    DateOfBirth = new DateOnly(1998, 6, 10)
                }
            });

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    public DbSet<FollowerPair> FollowerPairs { get; set; }
}
