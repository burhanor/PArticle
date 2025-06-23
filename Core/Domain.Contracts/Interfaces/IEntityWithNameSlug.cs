using Domain.Contracts.Enums;

namespace Domain.Contracts.Interfaces
{
	public interface IEntityWithNameSlug:IEntityBase
	{
		string Name { get; set; }
		string Slug { get; set; }
		Status Status { get; set; }
	}
}