using BisleriumPvtLtdBackendSample1.Models;
using Microsoft.EntityFrameworkCore;

namespace BisleriumPvtLtdBackendSample1
{
    public class BisleriumBlogDbContext:DbContext
    {
        public BisleriumBlogDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogReaction>()
                .HasOne(br => br.User)
                .WithMany()
                .HasForeignKey(br => br.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Comment>()
               .HasOne(br => br.User)
               .WithMany()
               .HasForeignKey(br => br.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany()
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Roles> Roles{ get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Blog> Blogs{ get; set; }
        public DbSet<ReactionType> ReactionTypes { get; set; }
        public DbSet<BlogReaction> BlogReactions  { get; set; }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<CommentReaction> CommentReactions{ get; set; }
    }
}
