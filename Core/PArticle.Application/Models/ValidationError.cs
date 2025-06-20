namespace PArticle.Application.Models
{
	public class ValidationError
	{
		public ValidationError()
		{

		}
		public ValidationError(string propertyName, string errorMessage)
		{
			PropertyName = propertyName;
			ErrorMessage = errorMessage;
		}

		public string PropertyName { get; set; } = string.Empty;
		public string ErrorMessage { get; set; } = string.Empty;
	}
}
