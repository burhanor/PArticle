using Domain.Contracts.Enums;

namespace PArticle.Application.Models.Category
{
	public class CategoryDto
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
	}
}
