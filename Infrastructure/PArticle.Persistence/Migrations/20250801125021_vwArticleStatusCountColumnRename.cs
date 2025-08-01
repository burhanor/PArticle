using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwArticleStatusCountColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwArticleStatusCount] AS
                                    Select Status,COUNT(*) as [Count] from Articles
                                    GROUP BY Status
                                    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwArticleStatusCount] AS
                                    Select Status,COUNT(*) as ArticleCount from Articles
                                    GROUP BY Status
                                    ");
		}
    }
}
