using CodePulseAPI.Models.Domain;
using CodePulseAPI.Models.DTO;
using CodePulseAPI.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
		{
			this.imageRepository = imageRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllImages() {

			var images = await imageRepository.GetAllImages();

			var response = new List<BlogImageDTO>();

			foreach (var image in images)
			{
				response.Add(new BlogImageDTO { 
					Id = image.Id,
					Title = image.Title,
					FileName = image.FileName,
					FileExtension = image.FileExtension,
					DateCreated = image.DateCreated,
					Url = image.Url
				});
			}

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> UploadImage(
			[FromForm] IFormFile file, 
			[FromForm] string fileName,
			[FromForm] string title
			)
		{
			ValidateFileUpload(file);

			if(ModelState.IsValid)
			{
				var blogImage = new BlogImage { 
					FileExtension = Path.GetExtension(file.FileName),
					FileName = fileName,
					Title = title ,
					DateCreated = DateTime.Now
				};

				blogImage = await imageRepository.Upload(file, blogImage);
				
				var response = new BlogImageDTO { 
					Id = blogImage.Id,
					FileName = blogImage.FileName,
					Title = blogImage.Title,
					FileExtension = blogImage.FileExtension,
					DateCreated = blogImage.DateCreated ,
					Url = blogImage.Url
				};
					return Ok(response);
			}

			return BadRequest(ModelState);
		}

		private void ValidateFileUpload(IFormFile file)
		{
			var allowedExtension = new string[] {".jpg",".jpeg",".png"};


			if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
			{
				ModelState.AddModelError("file", "Unsupported file format");
			}
			if(file.Length > 10485760) {
				ModelState.AddModelError("file", "File size cannot be than 10MB");
			}
		}
	}
}
