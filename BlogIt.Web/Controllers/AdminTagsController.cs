using Azure;
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

            return RedirectToAction("ListAll");
        }

        [HttpGet]
        [ActionName("ListAll")]
        public IActionResult ListAll()
        {
            // use dbContext to read the tags
            var tags = blogItDbContext.Tags.ToList();

            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = blogItDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = blogItDbContext.Tags.Find(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                // Save Changes
                blogItDbContext.SaveChanges();
                return RedirectToAction("ListAll");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }


        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var existingTag = blogItDbContext.Tags.Find(editTagRequest.Id);

            if (existingTag != null)
            {
                blogItDbContext.Tags.Remove(existingTag);
                blogItDbContext.SaveChanges();
                return RedirectToAction("ListAll");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
    }
}
