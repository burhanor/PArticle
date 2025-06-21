using FluentValidation;
using FluentValidation.Results;
using PArticle.Application.Extentions;
using PArticle.Application.Models;

namespace PArticle.Application.Helpers
{
	public static class ValidationHelper
	{

		public static async Task<ResponseContainer<TResponse>> ValidateAsync<TRequest,TResponse,TValidator>(TRequest request,CancellationToken cancellationToken) where TValidator:AbstractValidator<TRequest>,new()
		{
			TValidator validator = new();
			ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
			ResponseContainer<TResponse> response = validationResult.ToResponse<TResponse>();
			return response;
		}

	}
}
