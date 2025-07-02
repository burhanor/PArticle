using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwTopRatedArticles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwTopRatedArticles] AS
                                    SELECT 
                                        a.Title,
                                        a.Slug,
                                        ROW_NUMBER() OVER (ORDER BY av.TotalVoteCount DESC) AS DisplayOrder 
                                    FROM Articles a 
                                    LEFT JOIN vwArticleVotes av on a.Id=av.ArticleId and av.Vote=1
                                    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwTopRatedArticles]");

		}
	}
}
