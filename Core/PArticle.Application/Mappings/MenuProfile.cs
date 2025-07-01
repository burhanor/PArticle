using AutoMapper;
using PArticle.Application.Features.Menu.Commands.CreateMenuItem;
using PArticle.Application.Features.Menu.Commands.UpdateMenuItem;
using PArticle.Application.Features.Menu.Queries.GetMenuItem;
using PArticle.Application.Features.Menu.Queries.GetMenuItems;
using PArticle.Application.Models.Menu;

namespace PArticle.Application.Mappings
{
	internal class MenuProfile: Profile
	{
		public MenuProfile()
		{
			CreateMap<MenuDto, CreateMenuItemCommandRequest>();
			CreateMap<CreateMenuItemCommandRequest, Domain.Entities.MenuItem>();
			CreateMap<Domain.Entities.MenuItem, CreateMenuItemCommandResponse>();

			CreateMap<UpdateMenuItemCommandRequest, Domain.Entities.MenuItem>();
			CreateMap<MenuDto, UpdateMenuItemCommandRequest>();
			CreateMap<Domain.Entities.MenuItem, UpdateMenuItemCommandResponse>();


			CreateMap<Domain.Entities.MenuItem, GetMenuItemQueryResponse>();

			CreateMap<Domain.Entities.MenuItem, GetMenuItemsQueryResponse>();
			CreateMap<MenuFilterModel, GetMenuItemsQueryRequest>();
		}
	}
}
