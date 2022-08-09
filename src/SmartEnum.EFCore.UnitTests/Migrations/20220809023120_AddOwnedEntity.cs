using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEnum.EFCore.IntegrationTests.Migrations
{
    public partial class AddOwnedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Owned1Value",
                table: "SomeEntities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Owned1Weekday",
                table: "SomeEntities",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owned1Value",
                table: "SomeEntities");

            migrationBuilder.DropColumn(
                name: "Owned1Weekday",
                table: "SomeEntities");
        }
    }
}
