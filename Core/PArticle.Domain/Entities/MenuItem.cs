using Domain.Contracts.Enums;
using PArticle.Domain.Concretes;

namespace PArticle.Domain.Entities
{
	public class MenuItem:EntityBase
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public MenuType MenuType { get; set; }
	}
}
