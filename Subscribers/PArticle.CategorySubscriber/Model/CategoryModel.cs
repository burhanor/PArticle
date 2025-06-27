using PArticle.Subscribers.Core.Interfaces;

namespace PArticle.CategorySubscriber.Model
{
	
	public class CategoryModel : IId
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int Status { get; set; }
	}
}
