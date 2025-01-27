using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketing.Migrations
{
    /// <inheritdoc />
    public partial class addReportProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReportedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportProfiles_AspNetUsers_ReportedById",
                        column: x => x.ReportedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReportProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportProfiles_ReportedById",
                table: "ReportProfiles",
                column: "ReportedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReportProfiles_UserId",
                table: "ReportProfiles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportProfiles");
        }
    }
}
