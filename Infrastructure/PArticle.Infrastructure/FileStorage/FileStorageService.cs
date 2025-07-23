using Microsoft.AspNetCore.Hosting;
using PArticle.Application.Abstractions.Interfaces.FileStorage;

namespace PArticle.Infrastructure.FileStorage
{

	public class FileStorageService : IFileStorageService
	{
		private readonly IWebHostEnvironment _env;

		public FileStorageService(IWebHostEnvironment env)
		{
			_env = env;
		}

		public async Task<string> SaveFileAsync(Stream content, string fileName)
		{
			var folderPath = Path.Combine(_env.WebRootPath, "images");
			string newFileName = $"{Guid.NewGuid()}_{fileName}";
			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var filePath = Path.Combine(folderPath, newFileName);
			await using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				await content.CopyToAsync(fileStream);
			}
			return $"/images/{newFileName}";
		}
	}

}
