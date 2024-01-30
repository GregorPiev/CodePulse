using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories.Implementation;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository _blogpodtRepository;
        private readonly ICategoryRepository _categoryRepository;
        // private object response;

        public BlogPostController(IBlogPostRepository blogpodtRepository, ICategoryRepository categoryRepository)
        {
            _blogpodtRepository = blogpodtRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogPostList()
        {
            var blogPostList =await _blogpodtRepository.GetAllAsync();
            
            var response = new List<BlogPostDTO>();

            foreach (var blogPostItem in blogPostList)
            {
                response.Add(new BlogPostDTO { 
                 Id = blogPostItem.Id,
                 Author = blogPostItem.Author,
                 Content = blogPostItem.Content,
                 FeaturedImageUrl = blogPostItem.FeaturedImageUrl,
                 IsVisible = blogPostItem.IsVisible,
                 PublisheDate = blogPostItem.PublisheDate,
                 ShortDescription = blogPostItem.ShortDescription,
                 Title = blogPostItem.Title,
                 UrlHandle = blogPostItem.UrlHandle,
                 Categories = blogPostItem.Categories.Select(x => new CategoryDTO
                  {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                  }).ToList()
                });
            }
            return Ok(response);
        }


        [HttpPost]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDTO request) 
        {
            var blogPost = new BlogPost 
            {  
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublisheDate = request.PublisheDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach(var categoryGuid in request.Categories) 
            { 
               var existingCategory = await _categoryRepository.GetByIdAsync(categoryGuid);
                if(existingCategory is not null)
                {
                    blogPost.Categories.Add(existingCategory);
                }

            }

            blogPost = await _blogpodtRepository.CreateAsync(blogPost);

            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublisheDate = blogPost.PublisheDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDTO
                { 
                    Id = x.Id, 
                    Name = x.Name, 
                    UrlHandle = x.UrlHandle 
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute]Guid id)
        {
            var blogPost =await _blogpodtRepository.GetByIdAsync(id);
            
           if(blogPost is  null)
            {
                return NotFound();
            }

            var response = new BlogPostDTO
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublisheDate = blogPost.PublisheDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogPostByURLHandle([FromRoute]string urlHandle) {
			var blogPost = await _blogpodtRepository.GetByURLHandleAsync(urlHandle);

			if (blogPost is null)
			{
				return NotFound();
			}

			var response = new BlogPostDTO
			{
				Id = blogPost.Id,
				Author = blogPost.Author,
				Content = blogPost.Content,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				IsVisible = blogPost.IsVisible,
				PublisheDate = blogPost.PublisheDate,
				ShortDescription = blogPost.ShortDescription,
				Title = blogPost.Title,
				UrlHandle = blogPost.UrlHandle,
				Categories = blogPost.Categories.Select(x => new CategoryDTO
				{
					Id = x.Id,
					Name = x.Name,
					UrlHandle = x.UrlHandle
				}).ToList()
			};

			return Ok(response);
		}

        [HttpPut]
        [Route("{id:Guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> UpdateBlogPostById([FromRoute]Guid id, [FromBody]UpdateBlogPostRequestDTO request)
        {
            var blogPost = new BlogPost
            {
                Id = id,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublisheDate = request.PublisheDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach (var categoryGuid in request.Categories) {
                var existingCategory = await _categoryRepository.GetByIdAsync(categoryGuid); 
                
                if(existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);

                }
            }

            var updateBlogPost = await _blogpodtRepository.UpdateAsync(blogPost);

            if(updateBlogPost == null) {
                return NotFound();
            }

            var response = new BlogPostDTO
            {
                Id = updateBlogPost.Id,
                Author = updateBlogPost.Author,
                Content = updateBlogPost.Content,
                FeaturedImageUrl = updateBlogPost.FeaturedImageUrl,
                IsVisible = updateBlogPost.IsVisible,
                PublisheDate = updateBlogPost.PublisheDate,
                ShortDescription = updateBlogPost.ShortDescription,
                Title = updateBlogPost.Title,
                UrlHandle = updateBlogPost.UrlHandle,
                Categories = updateBlogPost.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> DeleteBlogPost([FromRoute]Guid id)
        {
            var blogPostResult = await _blogpodtRepository.DeleteAsync(id); 
            
            if(blogPostResult is null) { 
                return NotFound(); 
            }

            var response = new BlogPostDTO
            {
                Id = blogPostResult.Id,
                Author = blogPostResult.Author,
                Content = blogPostResult.Content,
                FeaturedImageUrl = blogPostResult.FeaturedImageUrl,
                IsVisible = blogPostResult.IsVisible,
                PublisheDate = blogPostResult.PublisheDate,
                ShortDescription = blogPostResult.ShortDescription,
                Title = blogPostResult.Title,
                UrlHandle = blogPostResult.UrlHandle               
            };

            if(blogPostResult.Categories is not null)
            {
                response.Categories = blogPostResult.Categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList();

			}

            return Ok(response);
        }
    }
}
