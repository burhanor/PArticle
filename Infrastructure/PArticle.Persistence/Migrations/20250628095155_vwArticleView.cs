using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwArticleView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER VIEW [dbo].[vwArticleViews] AS
                SELECT 
                    ArticleId,
                    COUNT(*) AS TotalViewCount
                    FROM (
                        SELECT 
                            ArticleId,
                            CONVERT(date, ViewDate) AS ViewDate,
                            IpAddress
                        FROM ArticleViews
                            GROUP BY 
                                ArticleId,
                                CONVERT(date, ViewDate),
                                IpAddress
                        ) AS GroupedDailyViews
                    GROUP BY ArticleId;");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwArticleViews]");

		}
	}
}
