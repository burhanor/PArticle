namespace PArticle.Shared.Models
{
	public class GetTopAuthor
	{
		public string Nickname { get; set; }
		public int PendingCount { get; set; }
		public int PublishedCount { get; set; }
		public int RejectedCount { get; set; }
		public int TotalCount { get; set; }
	}
}
