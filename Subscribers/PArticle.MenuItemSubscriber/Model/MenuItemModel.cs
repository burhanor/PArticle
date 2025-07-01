using PArticle.Subscribers.Core.Interfaces;

namespace PArticle.MenuItemSubscriber.Model
{
	public class MenuItemModel : IId
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Link { get; set; }
		public int MenuType { get; set; }
		public int DisplayOrder { get; set; }
	}
}
