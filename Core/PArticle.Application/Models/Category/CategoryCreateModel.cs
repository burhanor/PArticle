using PArticle.Domain.Enums;

namespace PArticle.Application.Models.Category
{
	public class CategoryCreateModel
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
	}
}
