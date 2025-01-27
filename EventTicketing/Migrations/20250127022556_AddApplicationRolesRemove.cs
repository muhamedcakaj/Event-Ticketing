using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketing.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationRolesRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }
    }
}
