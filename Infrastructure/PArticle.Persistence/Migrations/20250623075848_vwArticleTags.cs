using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwArticleTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
                CREATE OR ALTER VIEW [dbo].[vwArticleTags] AS
                SELECT at.ArticleId, t.Id, t.Name, t.Slug, t.Status 
                FROM ArticleTags at 
                INNER JOIN Tags t ON at.TagId = t.Id
            ");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwArticleTags]");
		}
    }
}
