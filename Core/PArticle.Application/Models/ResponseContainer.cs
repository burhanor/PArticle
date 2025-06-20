using PArticle.Application.Enums;

namespace PArticle.Application.Models
{
	public class ResponseContainer<T>
	{
		public T? Data { get; set; }
		public string? Message { get; set; }
		public ResponseStatus Status { get; set; }
		public List<ValidationError> ValidationErrors { get; set; } = [];



		internal void AddValidationError(string propertyName, string errorMessage)
		{
			ValidationErrors.Add(new ValidationError(propertyName, errorMessage));
		}
	}
}
