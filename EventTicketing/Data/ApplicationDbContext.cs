using EventTicketing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace EventTicketing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserRelationship> UserRelationships { get; set; }
        public DbSet<Repost> Reposts { get; set; } 
        public DbSet<Likes> Likes { get; set; } 
        public DbSet<SavePost> SavePosts { get; set; }
        public DbSet<Notifications> Notifications { get; set; }

        public DbSet<ProfileViewer> ProfileViewers { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ReportProfile> ReportProfiles { get; set; }

        public DbSet<ReportPosts> ReportPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite key
            modelBuilder.Entity<UserRelationship>()
                .HasKey(ur => new { ur.FollowerId, ur.FollowedId });

            modelBuilder.Entity<UserRelationship>()
                .HasOne(ur => ur.FollowerUser)
                .WithMany()
                .HasForeignKey(ur => ur.FollowerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserRelationship>()
                .HasOne(ur => ur.FollowedUser)
                .WithMany()
                .HasForeignKey(ur => ur.FollowedId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    
    }
}