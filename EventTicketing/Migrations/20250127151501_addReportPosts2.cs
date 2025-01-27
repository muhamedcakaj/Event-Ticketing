using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketing.Migrations
{
    /// <inheritdoc />
    public partial class addReportPosts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportPosts_PostId",
                table: "ReportPosts",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportPosts");
        }
    }
}
