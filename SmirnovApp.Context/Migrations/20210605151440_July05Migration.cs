using Microsoft.EntityFrameworkCore.Migrations;

namespace SmirnovApp.Context.Migrations
{
    public partial class July05Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Persons_ClientId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Persons_EmployeeId",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "ServicesDirection",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceCategory",
                table: "Services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Persons_ClientId",
                table: "Contracts",
                column: "ClientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Persons_EmployeeId",
                table: "Contracts",
                column: "EmployeeId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Persons_ClientId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Persons_EmployeeId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ServicesDirection",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ServiceCategory",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Persons");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Persons_ClientId",
                table: "Contracts",
                column: "ClientId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Persons_EmployeeId",
                table: "Contracts",
                column: "EmployeeId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
