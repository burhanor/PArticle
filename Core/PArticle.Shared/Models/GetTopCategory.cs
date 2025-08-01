using Domain.Contracts.Enums;

namespace PArticle.Shared.Models
{
	public class GetTopCategory
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }
		public int Count { get; set; }
	}
}
