using BlogIt.Web.Data;
using BlogIt.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogIt.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogItDbContext blogItDbContext;

        public BlogPostLikeRepository(BlogItDbContext blogItDbContext)
        {
            this.blogItDbContext = blogItDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await blogItDbContext.BlogPostLike.AddAsync(blogPostLike);
            await blogItDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await blogItDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await blogItDbContext.BlogPostLike
                .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
