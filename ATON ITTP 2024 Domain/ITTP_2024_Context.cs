using ATON_ITTP_2024_Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace ATON_ITTP_2024_Domain
{
    public class ITTP_2024_Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
        public DbSet<UserResponse> UserResponses { get; set; }

        public ITTP_2024_Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(p => new { p.Login }).IsUnique(true);

            modelBuilder.Entity<User>().HasIndex(p => new { p.Token }).IsUnique(true);
        }
    }
}
