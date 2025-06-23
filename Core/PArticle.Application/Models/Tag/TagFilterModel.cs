using Domain.Contracts.Enums;

namespace PArticle.Application.Models.Tag
{
	public class TagFilterModel
	{
		public string? Name { get; set; }
		public string? Slug { get; set; }
		public Status? Status { get; set; }
	}
}
