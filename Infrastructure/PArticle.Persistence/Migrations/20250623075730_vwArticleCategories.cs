using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwArticleCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
                CREATE OR ALTER VIEW [dbo].[vwArticleCategories] AS
                SELECT ac.ArticleId, c.Id, c.Name, c.Slug, c.Status 
                FROM ArticleCategories ac 
                INNER JOIN Categories c ON ac.CategoryId = c.Id
            ");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {

			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwArticleCategories]");
		}
	}
}
