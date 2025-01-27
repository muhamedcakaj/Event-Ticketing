using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketing.Migrations
{
    /// <inheritdoc />
    public partial class addReportPosts3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUser",
                table: "ReportPosts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReportPosts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ReportPosts_UserId",
                table: "ReportPosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportPosts_AspNetUsers_UserId",
                table: "ReportPosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportPosts_AspNetUsers_UserId",
                table: "ReportPosts");

            migrationBuilder.DropIndex(
                name: "IX_ReportPosts_UserId",
                table: "ReportPosts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReportPosts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser",
                table: "ReportPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
