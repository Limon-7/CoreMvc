using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentMvc.Migrations
{
    public partial class ExtendedStudentClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Students",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Department", "Email", "Name", "PhotoPath" },
                values: new object[] { 4, 3, "abc@gmail.com", "Pinic", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Students");
        }
    }
}
