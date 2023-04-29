using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzengineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_CoverProperty_Pizza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Pizzas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Pizzas");
        }
    }
}
