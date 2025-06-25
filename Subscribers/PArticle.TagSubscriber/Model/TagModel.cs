using PArticle.Subscribers.Core.Interfaces;

namespace PArticle.TagSubscriber.Model
{
	public class TagModel:IId
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Slug { get; set; }
		public int Status { get; set; }
	}
}
