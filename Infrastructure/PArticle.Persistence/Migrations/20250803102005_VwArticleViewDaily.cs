using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VwArticleViewDaily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwArticleViewDaily] AS 
                                    SELECT
                                      CAST(ViewDate AS DATE) AS ViewDay,
                                      COUNT(*) AS TotalViews,
                                      COUNT(DISTINCT IpAddress) AS UniqueViews
                                    FROM 
                                      articleviews
                                    GROUP BY 
                                      CAST(ViewDate AS DATE)
                                    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwArticleViewDaily]");
		}
    }
}
