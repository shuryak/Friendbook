using Microsoft.EntityFrameworkCore;
using Friendbook.DataAccess.PostgreSql.Entities;

namespace Friendbook.DataAccess.PostgreSql;

public sealed class FriendbookDbContext : DbContext
{
    public FriendbookDbContext(DbContextOptions<FriendbookDbContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<UserProfile>()
        //     .HasData(new UserProfile
        //     {
        //         Id = 1,
        //         Nickname = "sanya100",
        //         FirstName = "Alexander",
        //         LastName = "Petrov",
        //         DateOfBirth = new DateOnly(2001, 12, 28),
        //     }, new UserProfile
        //     {
        //         Id = 2,
        //         Nickname = "ivan03vdovin",
        //         FirstName = "Ivan",
        //         LastName = "Vdovin",
        //         DateOfBirth = new DateOnly(2003, 2, 16)
        //     }, new UserProfile
        //     {
        //         Id = 3,
        //         Nickname = "archii",
        //         FirstName = "Artur",
        //         LastName = "Ivanov",
        //         DateOfBirth = new DateOnly(1998, 6, 10)
        //     });

        // modelBuilder.Entity<FollowerPair>()
        //     .HasData(new FollowerPair
        //     {
        //         Id = 1,
        //         FollowerId = 1,
        //         FollowingId = 2
        //     });

        // base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    public DbSet<FollowerPair> FollowerPairs => Set<FollowerPair>();
}
