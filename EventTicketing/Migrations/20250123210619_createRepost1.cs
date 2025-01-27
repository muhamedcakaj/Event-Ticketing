using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketing.Migrations
{
    /// <inheritdoc />
    public partial class createRepost1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            {
                migrationBuilder.CreateTable(
                    name: "Reposts",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        OriginalPostId = table.Column<int>(type: "int", nullable: false),
                        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Reposts", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Reposts_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_Reposts_Posts_OriginalPostId",
                            column: x => x.OriginalPostId,
                            principalTable: "Posts",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_Reposts_OriginalPostId",
                    table: "Reposts",
                    column: "OriginalPostId");

                migrationBuilder.CreateIndex(
                    name: "IX_Reposts_UserId",
                    table: "Reposts",
                    column: "UserId");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
