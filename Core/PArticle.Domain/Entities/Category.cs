using Domain.Contracts.Enums;
using Domain.Contracts.Interfaces;
using PArticle.Domain.Concretes;

namespace PArticle.Domain.Entities
{
	public class Category:EntityBase,IEntityWithNameSlug
	{
		public string Name { get; set; }
		public string Slug { get; set; }
		public Status Status { get; set; }

		public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
	}
}
