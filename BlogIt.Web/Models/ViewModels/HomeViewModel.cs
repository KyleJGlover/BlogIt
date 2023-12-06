using BlogIt.Web.Models.Domain;

namespace BlogIt.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
