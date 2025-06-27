namespace PArticle.Subscribers.Core.Models
{
	public class LogModel
	{
		public string AppName { get; set; }
		public string MethodName { get; set; }
		public string Message { get; set; }
		public DateTime Date { get;} = DateTime.Now;
	}
}
