using Microsoft.EntityFrameworkCore.Migrations;

namespace Gruppo3Esame.Migrations
{
    public partial class Mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Employee_EmployeeId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_EmployeeId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Project");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_EmployeeId",
                table: "Project",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Employee_EmployeeId",
                table: "Project",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
