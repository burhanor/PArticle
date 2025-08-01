using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwTagStatusCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwTagStatusCount] AS
                                    Select Status,COUNT(*) as [Count] from Tags
                                    GROUP BY Status
                                    ");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwTagStatusCount]");
		}
    }
}
