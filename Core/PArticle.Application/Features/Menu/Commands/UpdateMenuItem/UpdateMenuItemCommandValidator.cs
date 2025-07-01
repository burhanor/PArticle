using FluentValidation;
using PArticle.Application.Constants;

namespace PArticle.Application.Features.Menu.Commands.UpdateMenuItem
{
	internal class UpdateMenuItemCommandValidator : AbstractValidator<UpdateMenuItemCommandRequest>
	{
		public UpdateMenuItemCommandValidator()
		{
			RuleFor(m => m.Title)
				.NotEmpty().WithMessage(Messages.MenuItem.MENU_ITEM_TITLE_REQUIRED)
				.MaximumLength(50).WithMessage(Messages.MenuItem.MENU_ITEM_TITLE_MAX_LENGTH);
			RuleFor(m => m.Description)
				.MaximumLength(100).WithMessage(Messages.MenuItem.MENU_ITEM_DESCRIPTION_MAX_LENGTH);
			RuleFor(m => m.Link)
				.NotEmpty().WithMessage(Messages.MenuItem.MENU_ITEM_LINK_REQUIRED)
				.MaximumLength(100).WithMessage(Messages.MenuItem.MENU_ITEM_LINK_MAX_LENGTH);
			RuleFor(m => m.MenuType)
				.IsInEnum().WithMessage(Messages.MenuItem.MENU_ITEM_TYPE_INVALID);
		}
	}
}
