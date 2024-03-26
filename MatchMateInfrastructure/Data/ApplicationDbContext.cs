using MatchMateInfrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MatchMateInfrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<UserInterest> UsersInterests { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BlockedUsers> BlockedUsers { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ReportedOffer> ReportedOffers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserInterest>()
                .HasKey(ui => new { ui.InterestId, ui.UserId });

            builder.Entity<BlockedUsers>()
                .HasOne(bu => bu.BlockerUser)
                .WithMany(au => au.BlockedUsersByMe)
                .HasForeignKey(bu => bu.BlockerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(f => f.Sender)
                .WithMany(s => s.SendFriendships)
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m=>m.Sender)
                .WithMany(s=>s.SendedMessages)
                .HasForeignKey(m=>m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Offer>()
                .HasOne(o=>o.SuggestingUser)
                .WithMany(su=>su.SuggestedOffers)
                .HasForeignKey(o=>o.SuggestingUserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}