using FluentValidation;
using PArticle.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Tag.Commands.CreateTag
{
	internal class CreateTagCommandValidator:AbstractValidator<CreateTagCommandRequest>
	{
		public CreateTagCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty().WithMessage(Messages.Tag.TAG_NAME_REQUIRED)
				.MaximumLength(100).WithMessage(Messages.Tag.TAG_NAME_MAX_LENGTH);
			RuleFor(m => m.Slug)
				.NotEmpty().WithMessage(Messages.Tag.TAG_SLUG_REQUIRED)
				.MaximumLength(50).WithMessage(Messages.Tag.TAG_SLUG_MAX_LENGTH);
			RuleFor(m => m.Status)
				.IsInEnum().WithMessage(Messages.Tag.TAG_STATUS_INVALID);
		}
	}
}
