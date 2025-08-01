using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GetTopTagsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
        CREATE OR ALTER PROCEDURE GetTopTags
            @N INT =5
        AS
        BEGIN
            SELECT TOP (@N)
                t.Name,
                t.Slug,
                COUNT(*) AS [Count]
            FROM Tags t
            INNER JOIN ArticleTags at ON t.Id = at.TagId
            GROUP BY t.Name, t.Slug
            ORDER BY [Count] DESC
        END
    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
        CREATE OR ALTER PROCEDURE GetTopTags
            @N INT
        AS
        BEGIN
            SELECT TOP (@N)
                t.Name,
                t.Slug,
                COUNT(*) AS [Count]
            FROM Tags t
            INNER JOIN ArticleTags at ON t.Id = at.TagId
            GROUP BY t.Name, t.Slug
            ORDER BY [Count] DESC
        END
    ");
		}
    }
}
