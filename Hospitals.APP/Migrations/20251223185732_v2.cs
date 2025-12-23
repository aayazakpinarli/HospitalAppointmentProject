using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospitals.APP.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Doctors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Doctors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
