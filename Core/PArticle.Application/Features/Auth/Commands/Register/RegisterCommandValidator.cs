using FluentValidation;
using PArticle.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PArticle.Application.Features.Auth.Commands.Register
{
	internal class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
	{
		public RegisterCommandValidator()
		{
			RuleFor(m => m.EmailAddress)
				.NotEmpty().WithMessage(Messages.Auth.INVALID_EMAIL)
				.MaximumLength(50).WithMessage(Messages.Auth.EMAIL_MAX_LENGTH)
				.EmailAddress().WithMessage(Messages.Auth.INVALID_EMAIL);
			RuleFor(m => m.Nickname)
				.NotEmpty().WithMessage(Messages.Auth.INVALID_NICKNAME)
				.MaximumLength(50).WithMessage(Messages.Auth.NICKNAME_MAX_LENGTH);
			RuleFor(m => m.Password).NotEmpty().WithMessage(Messages.Auth.INVALID_PASSWORD);
		}
	}
}
