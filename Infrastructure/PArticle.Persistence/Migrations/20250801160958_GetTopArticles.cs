using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GetTopArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
			CREATE OR ALTER PROCEDURE GetTopArticles
                @StartDate DATETIME,
                @EndDate DATETIME,
                @N INT
            AS
            BEGIN
                SET NOCOUNT ON;

                WITH DistinctViews AS (
                    SELECT DISTINCT 
                        ArticleId,
                        CAST(ViewDate AS DATE) AS ViewDate,
                        IpAddress
                    FROM ArticleViews
                    WHERE ViewDate BETWEEN @StartDate AND @EndDate
                )
                SELECT TOP (@N)
	            u.Nickname,
                    a.Title,
                    a.Slug,
                    COUNT(*) AS ViewCount
                FROM DistinctViews dv
                INNER JOIN Articles a ON a.Id = dv.ArticleId
	            INNER JOIN Users u on u.Id=a.UserId
                GROUP BY a.Title, a.Slug,u.Nickname
                ORDER BY ViewCount DESC;
            END
            ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

			migrationBuilder.Sql("DROP PROCEDURE GetTopArticles");
		}
    }
}
