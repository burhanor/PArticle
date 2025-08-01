using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GetArticleRates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
			CREATE OR ALTER PROCEDURE GetArticleRates
                @StartDate DATETIME,
                @EndDate DATETIME,
                @N INT
            AS
            BEGIN
                SET NOCOUNT ON;
                WITH UniqueVotes AS (
                    SELECT
                        ArticleId,
                        CAST(VoteDate AS DATE) AS VoteDay,
                        IpAddress,
                        MAX(Vote) AS Vote  
                    FROM ArticleVotes
                    WHERE VoteDate BETWEEN @StartDate AND @EndDate
                    GROUP BY ArticleId, CAST(VoteDate AS DATE), IpAddress
                )
            
                SELECT TOP (@N)
                    u.Nickname,
                    a.Title,
                    a.Slug,
                    AVG(CAST(v.Vote AS FLOAT)) AS AverageVote
                FROM UniqueVotes v
                INNER JOIN Articles a ON a.Id = v.ArticleId
                INNER JOIN Users u ON u.Id = a.UserId
                GROUP BY u.Nickname,a.Title, a.Slug
                ORDER BY AverageVote DESC;
            END
            ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP PROCEDURE GetArticleRates");

		}
    }
}
