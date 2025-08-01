using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class GetTopCategories : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"
			CREATE OR ALTER PROCEDURE GetTopCategories
			    @N INT =5
			AS
			BEGIN
			    SELECT TOP (@N)
					c.Name,
					c.Slug,
					c.Status,
					COUNT(*) AS [Count]
				FROM Categories c
				INNER JOIN ArticleCategories at ON c.Id = at.CategoryId
				GROUP BY c.Name, c.Slug,c.Status
				ORDER BY [Count] DESC
			END
			");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP PROCEDURE GetTopCategories");

		}
	}
}
