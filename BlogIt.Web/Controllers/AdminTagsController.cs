using BlogIt.Web.Data;
using BlogIt.Web.Models.Domain;
using BlogIt.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogIt.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly BlogItDbContext blogItDbContext;

        public AdminTagsController(BlogItDbContext blogItDbContext)
        {
            this.blogItDbContext = blogItDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            // Map AddTagRequest to  Tag domain Model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            blogItDbContext.Tags.Add(tag);
            blogItDbContext.SaveChanges();

            return View("Add");
        }
    }
}
