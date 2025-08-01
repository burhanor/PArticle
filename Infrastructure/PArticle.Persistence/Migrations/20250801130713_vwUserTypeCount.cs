using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class vwUserTypeCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE OR ALTER VIEW [dbo].[vwUserTypeCount] AS
                                    Select UserType,IsActive,Count(*) as [Count] from Users
                                    group by UserType,IsActive
                                    ");
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

			migrationBuilder.Sql("DROP VIEW IF EXISTS [dbo].[vwUserTypeCount]");
		}
    }
}
