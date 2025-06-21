using FluentValidation;
using FluentValidation.Results;
using PArticle.Application.Enums;
using PArticle.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Extentions
{
	public static class ValidationExtension
	{
		public static ResponseContainer<T> ToResponse<T>(this ValidationResult validationResult)
		{
			ResponseContainer<T> response = new();
			if (!validationResult.IsValid)
			{
				if (!validationResult.IsValid)
				{
					response.ValidationErrors = [];
					var groupedErrors = validationResult.Errors
						.GroupBy(e => e.PropertyName)
						.Select(g => new ValidationError
						{
							PropertyName = g.Key,
							ErrorMessage = g.Select(e => e.ErrorMessage).FirstOrDefault() ?? string.Empty
						});
					response.ValidationErrors.AddRange(groupedErrors);
					if (response.ValidationErrors.Count > 0)
						response.Status = ResponseStatus.ValidationError;
				}
				return response;
			}
			return response;
		}


		
	}
}
