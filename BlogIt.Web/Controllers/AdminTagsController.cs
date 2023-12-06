using BlogIt.Web.Models.Domain;
using BlogIt.Web.Models.ViewModels;
using BlogIt.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogIt.Web.Controllers
{
    // Added a Tag repository to handle the async functional calls to simplify bussiness logic
    public class AdminTagsController : Controller
    {

        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            ValidateAddTagRequest(addTagRequest);

            if (ModelState.IsValid == false)
            {
                return View();
            }

            // Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("ListAll");
        }

        [HttpGet]
        [ActionName("ListAll")]
        public async Task<IActionResult> ListAll()
        {
            // use dbContext to read the tags
            var tags = await tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var existingTag = await tagRepository.GetAsync(id);

            if (existingTag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = existingTag.Id,
                    Name = existingTag.Name,
                    DisplayName = existingTag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = await tagRepository.UpdateAsync(tag);

            if (existingTag != null)
            {
                return RedirectToAction("ListAll");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                // Show an success notification
                return RedirectToAction("ListAll");
            }

            // Show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }
        private void ValidateAddTagRequest(AddTagRequest request)
        {
            if (request.Name is not null && request.DisplayName is not null)
            {
                if (request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }

    }

}
