using BlogIt.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogIt.Web.Data
{
    public class BlogItDbContext : DbContext
    {
        public BlogItDbContext(DbContextOptions<BlogItDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
        public DbSet<BlogPostComment> BlogPostComment { get; set; }
    }
}
