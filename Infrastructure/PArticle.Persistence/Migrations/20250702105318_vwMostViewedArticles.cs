using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwMostViewedArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwMostViewedArticles] AS 
                                    SELECT 
                                        a.Title,
                                        a.Slug,
                                        ROW_NUMBER() OVER (ORDER BY av.TotalViewCount DESC) AS DisplayOrder 
                                    FROM Articles a 
                                    LEFT JOIN vwArticleViews av on a.Id=av.ArticleId
                                    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwMostViewedArticles]");

		}
	}
}
