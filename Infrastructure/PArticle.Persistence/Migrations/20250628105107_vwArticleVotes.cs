using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwArticleVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwArticleVotes] AS 
                                    SELECT
                                        ArticleId,
                                        Vote,
                                        COUNT(*) AS TotalVoteCount
                                    FROM (
                                            SELECT 
                                                ArticleId,
                                                Vote,
                                                IpAddress
                                            FROM ArticleVotes
                                            GROUP BY 
                                                ArticleId,
                                                Vote,
                                                IpAddress
                                         ) AS DistinctDailyVotes
                                         GROUP BY 
                                            ArticleId,
                                            Vote;");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwArticleVotes]");

		}
    }
}
