using Microsoft.EntityFrameworkCore.Migrations;

namespace MInerdSchoolIntegration.Migrations
{
    public partial class Addstudentschoolrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolCode",
                table: "Student",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_SchoolCode",
                table: "Student",
                column: "SchoolCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_School_SchoolCode",
                table: "Student",
                column: "SchoolCode",
                principalTable: "School",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_School_SchoolCode",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_SchoolCode",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "SchoolCode",
                table: "Student");
        }
    }
}
