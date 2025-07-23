using FluentValidation;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.User.Commands.UpdateUser
{
	internal class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommandRequest>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(m => m.Id)
				.GreaterThan(0).WithMessage(Messages.User.USER_NOT_FOUND);
			RuleFor(m => m.Nickname)
				.NotEmpty().WithMessage(Messages.User.NICKNAME_REQUIRED)
				.MaximumLength(50).WithMessage(Messages.User.NICKNAME_MAX_LENGTH);
			RuleFor(m => m.EmailAddress)
				.NotEmpty().WithMessage(Messages.User.EMAIL_REQUIRED)
				.MaximumLength(100).WithMessage(Messages.User.EMAIL_MAX_LENGTH)
				.EmailAddress().WithMessage(Messages.Auth.INVALID_EMAIL);
			RuleFor(m => m.UserType)
				.IsInEnum().WithMessage(Messages.User.USER_TYPE_INVALID); 
			RuleFor(m => m.Image)
				.Must(image => image == null || image.ContentType.StartsWith("image/"))
				.WithMessage(Messages.User.INVALID_AVATAR);
		}
	}
}
