using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwCategoryStatusCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwCategoryStatusCount] AS
                                    Select Status,COUNT(*) as [Count] from Categories
                                    GROUP BY Status
                                    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwCategoryStatusCount]");
		}
    }
}
