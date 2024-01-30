using CodePulseAPI.Migrations;
using CodePulseAPI.Models.Domain;

namespace CodePulseAPI.Repositories.Interface
{
	public interface IImageRepository
	{
		Task<BlogImage>  Upload(IFormFile file, BlogImage blogImage);
		Task<IEnumerable<BlogImage>> GetAllImages();
	}
}
