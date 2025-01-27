﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketing.Migrations
{
    /// <inheritdoc />
    public partial class createLikes1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            {
                migrationBuilder.CreateTable(
                    name: "Likes",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        PostId = table.Column<int>(type: "int", nullable: false),
                        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Likes", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Likes_AspNetUsers_UserId",
                            column: x => x.UserId,
                            principalTable: "AspNetUsers",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_Likes_Posts_PostId",
                            column: x => x.PostId,
                            principalTable: "Posts",
                            principalColumn: "Id");
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_Likes_PostId",
                    table: "Likes",
                    column: "PostId");

                migrationBuilder.CreateIndex(
                    name: "IX_Likes_UserId",
                    table: "Likes",
                    column: "UserId");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
