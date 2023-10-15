using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Awktion.API.Migrations
{
    /// <inheritdoc />
    public partial class Rooms2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rooms",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Rooms",
                newName: "Id");
        }
    }
}
