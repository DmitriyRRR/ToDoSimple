using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoSimple.Domain.Migrations
{
    /// <inheritdoc />
    public partial class testUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Notes");
        }
    }
}
