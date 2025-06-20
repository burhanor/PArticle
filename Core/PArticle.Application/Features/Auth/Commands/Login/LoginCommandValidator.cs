using FluentValidation;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.Auth.Commands.Login
{
	public class LoginCommandValidator:AbstractValidator<LoginCommandRequest>
	{
		public LoginCommandValidator()
		{
			RuleFor(m => m.EmailOrNickname)
				.NotEmpty().WithMessage(Messages.Auth.INVALID_NICKNAME_OR_EMAIL);
			RuleFor(m => m.Password).NotEmpty().WithMessage(Messages.Auth.INVALID_PASSWORD);
		}
	}
}
