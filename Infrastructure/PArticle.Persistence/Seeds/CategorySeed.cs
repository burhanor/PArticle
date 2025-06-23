using Domain.Contracts.Enums;
using PArticle.Domain.Entities;

namespace PArticle.Persistence.Seeds
{
	internal static class CategorySeed
	{
		public static List<Category> Seed()
		{
			return [
				new Category
				{
					Id = 1,
					Name = "Technology",
					Slug = "technology",
					Status = Status.Published,
				},
				new Category
				{
					Id = 2,
					Name = "Health",
					Slug = "health",
					Status = Status.Published,
				},
				new Category
				{
					Id = 3,
					Name = "Lifestyle",
					Slug = "lifestyle",
					Status = Status.Published,
				},
				new Category
				{
					Id = 4,
					Name = "Education",
					Slug = "education",
					Status = Status.Published,
				},
				new Category
				{
					Id = 5,
					Name = "Travel",
					Slug = "travel",
					Status = Status.Rejected,
				},
				new Category
				{
					Id = 6,
					Name = "Food",
					Slug = "food",
					Status = Status.Rejected,
				},
				new Category
				{
					Id = 7,
					Name = "Finance",
					Slug = "finance",
					Status = Status.Rejected,
				},
				new Category
				{
				Id = 8,
				Name = "Entertainment",
				Slug = "entertainment",
				Status = Status.Rejected,
				},
				new Category
				{
				Id = 9,
				Name = "Sports",
				Slug = "sports",
				Status = Status.Pending,
				},
				new Category
				{
				Id = 10,
				Name = "Politics",
				Slug = "politics",
				Status = Status.Pending,
				},
				new Category
				{
				Id = 11,
				Name = "Business",
				Slug = "business",
				Status = Status.Pending,
				},
				new Category
				{
				Id = 12,
				Name = "Science",
				Slug = "science",
				Status = Status.Pending,
				},
				new Category
				{
				Id = 13,
				Name = "Art",
				Slug = "art",
				Status = Status.Pending,
				},
				new Category
				{
				Id = 14,
				Name = "Fashion",
				Slug = "fashion",
				Status = Status.Pending,
				},
				];
			
		}
	}
}
