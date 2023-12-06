using BlogIt.Web.Data;
using BlogIt.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogIt.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BlogItDbContext blogItDbContext;

        public BlogPostCommentRepository(BlogItDbContext bloggieDbContext)
        {
            this.blogItDbContext = bloggieDbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await blogItDbContext.BlogPostComment.AddAsync(blogPostComment);
            await blogItDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await blogItDbContext.BlogPostComment.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
