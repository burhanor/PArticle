using PArticle.Domain.Concretes;
using PArticle.Domain.Enums;

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
