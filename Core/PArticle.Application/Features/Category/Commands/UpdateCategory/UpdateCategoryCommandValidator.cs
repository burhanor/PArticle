using FluentValidation;
using PArticle.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Category.Commands.UpdateCategory
{
	public class UpdateCategoryCommandValidator:AbstractValidator<UpdateCategoryCommandRequest>
	{
		public UpdateCategoryCommandValidator()
		{
			RuleFor(m=>m.Id)
				.GreaterThan(0).WithMessage(Messages.Category.CATEGORY_NOT_FOUND);
			RuleFor(m => m.Name)
				.NotEmpty().WithMessage(Messages.Category.CATEGORY_NAME_REQUIRED)
				.MaximumLength(100).WithMessage(Messages.Category.CATEGORY_NAME_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(Messages.Category.CATEGORY_SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(Messages.Category.CATEGORY_SLUG_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(Messages.Category.CATEGORY_STATUS_INVALID);
		}
	}
}
