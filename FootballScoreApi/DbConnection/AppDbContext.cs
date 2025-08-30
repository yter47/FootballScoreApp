using FootballScoreApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballScoreApp.DbConnection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token)
                .IsUnique();

            modelBuilder.Entity<UserTeam>()
                .HasKey(ul => new { ul.UserId, ul.TeamId });

            modelBuilder.Entity<UserTeam>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ul => ul.UserId);

            modelBuilder.Entity<UserTeam>()
                .HasOne(ul => ul.Team)
                .WithMany()
                .HasForeignKey(ul => ul.TeamId);
        }

    }
}
