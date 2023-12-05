﻿using BlogIt.Web.Data;
using BlogIt.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogIt.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogItDbContext blogItDbContext;

        public BlogPostRepository(BlogItDbContext blogItDbContext)
        {
            this.blogItDbContext = blogItDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await blogItDbContext.AddAsync(blogPost);
            await blogItDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await blogItDbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                blogItDbContext.BlogPosts.Remove(existingBlog);
                await blogItDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await blogItDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await blogItDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await blogItDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await blogItDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await blogItDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}
