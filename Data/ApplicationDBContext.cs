using System;
using LearningApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LearningApi.Data {
    public class ApplicationDBContext : IdentityDbContext<AppUser> {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) { // adding register in episode 22
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole> { // need to run "dotnet ef migrations add SeedRoles" and "dotnet ef database update" to include this change (RMB TO SAVE)
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
