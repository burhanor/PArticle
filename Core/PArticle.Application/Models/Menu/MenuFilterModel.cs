using Domain.Contracts.Enums;

namespace PArticle.Application.Models.Menu
{
	public class MenuFilterModel:FilterModel
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Link { get; set; }
		public MenuType? MenuType { get; set; }
		public int? DisplayOrder { get; set; }
	}
}
