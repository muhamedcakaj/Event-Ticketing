using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketing.Migrations
{
    /// <inheritdoc />
    public partial class addingUserRelationship1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userRelationships",
                columns: table => new
                {
                    FollowerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowedId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRelationships", x => new { x.FollowerId, x.FollowedId });
                    table.ForeignKey(
                        name: "FK_userRelationships_AspNetUsers_FollowedId",
                        column: x => x.FollowedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_userRelationships_AspNetUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_userRelationships_FollowedId",
                table: "userRelationships",
                column: "FollowedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userRelationships");
        }
    }
}
