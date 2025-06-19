using PArticle.Domain.Entities;
using PArticle.Domain.Enums;

namespace PArticle.Persistence.Seeds
{
	internal static class TagSeed
	{
		public static List<Tag> Seed()
		{

			return [
													new Tag
													{
														Id = 1,
														Name = "Technology",
														Slug = "technology",
														Status = Status.Published,
													},
													new Tag
													{
														Id = 2,
														Name = "Health",
														Slug = "health",
														Status = Status.Published,
													},
													new Tag
													{
														Id = 3,
														Name = "Lifestyle",
														Slug = "lifestyle",
														Status = Status.Published,
													},
													new Tag
													{
														Id = 4,
														Name = "Education",
														Slug = "education",
														Status = Status.Published,
													},
													new Tag
													{
														Id = 5,
														Name = "Travel",
														Slug = "travel",
														Status = Status.Rejected,
													},
													new Tag
													{
														Id = 6,
														Name = "Food",
														Slug = "food",
														Status = Status.Rejected,
													},
													new Tag
													{
														Id = 7,
														Name = "Finance",
														Slug = "finance",
														Status = Status.Rejected,
													},
													new Tag
													{
														Id = 8,
														Name = "Entertainment",
														Slug = "entertainment",
														Status = Status.Rejected,
													},
													new Tag
													{
														Id = 9,
														Name = "Sports",
														Slug = "sports",
														Status = Status.Published,
													},
			];
		}
	}
}
