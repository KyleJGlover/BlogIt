using BlogIt.Web.Data;
using BlogIt.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogIt.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogItDbContext blogItDbContext;

        public TagRepository(BlogItDbContext blogItDbContext)
        {
            this.blogItDbContext = blogItDbContext;
        }


        public async Task<Tag> AddAsync(Tag tag)
        {
            await blogItDbContext.Tags.AddAsync(tag);
            await blogItDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await blogItDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                blogItDbContext.Tags.Remove(existingTag);
                await blogItDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await blogItDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return blogItDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await blogItDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await blogItDbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }
    }
}
