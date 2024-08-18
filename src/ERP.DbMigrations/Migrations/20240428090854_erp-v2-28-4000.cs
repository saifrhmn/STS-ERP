using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class erpv2284000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataDescription",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDescription",
                table: "Projects");
        }
    }
}
