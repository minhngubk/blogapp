using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VinodNair.Blog.Web.ViewModels;
using VinodNair.Blog.Infrastructure.Data.Repositories;
using VinodNair.Blog.Domain.Entities;
using VinodNair.Blog.Domain.Abstractions;
using VinodNair.Blog.Domain.Enums;
using VinodNair.Blog.Application.Contracts.Abstractions;

namespace VinodNair.Blog.Web.Controllers
{

    public class AdminBlogPostsController : BaseController
    {
        private readonly IBlogPostService _blogPostService;

        public AdminBlogPostsController(IBlogPostService blogPostService)
        {
            this._blogPostService = blogPostService;
        }


        [HttpGet]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Add()
        {
            // get tags from repository

            var model = new AddBlogPostRequest();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            if (ModelState.IsValid)
            {
                // Map view model to domain model
                var blogPost = new BlogPost
                {
                    Title = addBlogPostRequest.Title,
                    Content = addBlogPostRequest.Content,
                    BannerImageUrl = addBlogPostRequest.BannerImageUrl,
                    Author = addBlogPostRequest.Author,
                    Status = BlogStatus.Draft,
                    CreatedBy = LoggedInUserId ?? Guid.Empty,
                    CreatedDate = DateTime.UtcNow
                };

                await _blogPostService.InsertAsync(blogPost);

                return RedirectToAction("List");
            }
            // Show error notification
            return View(addBlogPostRequest);

        }



        [HttpGet]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> List()
        {
            // Call the repository
            var isEditor = LoggedInAccountType == UserRole.Editor;
            var editorId = isEditor ? LoggedInUserId : null;
            var blogPosts = await _blogPostService.GetBlogPostAsync(editorId);

            return View(blogPosts);
        }


        [HttpGet]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Retrive the result from repository
            var blogPost = await _blogPostService.FindAsync(x => x.Id == id);

            if (blogPost != null)
            {
                // map the domain model into the view model
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    BannerImageUrl = blogPost.BannerImageUrl,
                };

                return View(model);
            }

            // pass data to view
            return View(null);
        }


        [HttpPost]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            if (ModelState.IsValid)
            {
                var blogPost = new BlogPost
                {
                    Id = editBlogPostRequest.Id,
                    Title = editBlogPostRequest.Title,
                    Content = editBlogPostRequest.Content,
                    Author = editBlogPostRequest.Author,
                    BannerImageUrl = editBlogPostRequest.BannerImageUrl,
                };


                // Submit information for repository to update
                var updatedBlog = await _blogPostService.UpdateAsync(blogPost);

                if (updatedBlog != null)
                {
                    // Show success notification
                    return RedirectToAction("List");
                }
            }

            // Show error notification
            return View(editBlogPostRequest);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(EditBlogPostRequest editBlogPostRequest)
        {
            // Talk to repository to delete this blog post and tags
            var approvingBlogPost = await _blogPostService.Approve(editBlogPostRequest.Id);

            if (approvingBlogPost)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show error notification
            ViewData["ErrorMessage"] = "An error occurred please reload the page and try again";
            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(EditBlogPostRequest editBlogPostRequest)
        {
            // Talk to repository to delete this blog post and tags
            var RejectingBlogPost = await _blogPostService.Reject(editBlogPostRequest.Id);

            if (RejectingBlogPost)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show error notification
            ViewData["ErrorMessage"] = "An error occurred please reload the page and try again";
            return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
        }
    }
}
