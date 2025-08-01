using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PArticle.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GetTopAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
			CREATE OR ALTER PROCEDURE GetTopAuthors
			    @N INT =5
			AS
			BEGIN
			    SELECT TOP (@N)
                    u.Nickname,
                    SUM(CASE WHEN a.Status = 1 THEN 1 ELSE 0 END) AS PendingCount,
                    SUM(CASE WHEN a.Status = 2 THEN 1 ELSE 0 END) AS PublishedCount,
                    SUM(CASE WHEN a.Status = 3 THEN 1 ELSE 0 END) AS RejectedCount,
                    COUNT(*) AS TotalCount
                FROM Articles a
                INNER JOIN Users u on a.UserId=u.Id
                GROUP BY u.Nickname
                ORDER BY TotalCount DESC
			END
			");

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

			migrationBuilder.Sql("DROP PROCEDURE GetTopAuthors");
		}
    }
}
