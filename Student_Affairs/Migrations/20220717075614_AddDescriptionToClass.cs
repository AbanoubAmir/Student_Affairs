using Microsoft.EntityFrameworkCore.Migrations;

namespace Student_Affairs.Migrations
{
    public partial class AddDescriptionToClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Classes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Classes");
        }
    }
}
