using CodePulseAPI.Data;
using CodePulseAPI.Models.Domain;
using CodePulseAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Repositories.Implementation
{
	public class ImageRepository : IImageRepository
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly ApplicatinDbContext dbContext;

		public ImageRepository(
			IWebHostEnvironment webHostEnvironment, 
			IHttpContextAccessor httpContextAccessor,
			ApplicatinDbContext dbContext			
			) 
		{
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<BlogImage>> GetAllImages()
		{
			return await dbContext.BlogImages.ToListAsync();
			
		}

		public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
		{
			//1 - Upload the Image to API/Images
			var localPath = Path.Combine(webHostEnvironment.ContentRootPath,"Images",$"{blogImage.FileName}{blogImage.FileExtension}");
			using var stream = new FileStream(localPath, FileMode.Create);
			await file.CopyToAsync(stream);

			//2 - Update the database
			//https:\//codepulse.com/images/someFileName.jpg
			var httpRequest = httpContextAccessor.HttpContext.Request;
			var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

			blogImage.Url = urlPath;

			await dbContext.BlogImages.AddAsync( blogImage );
			await dbContext.SaveChangesAsync();

			return blogImage;
		}
	}
}
