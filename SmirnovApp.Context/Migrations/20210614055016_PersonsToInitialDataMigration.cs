using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmirnovApp.Context.Migrations
{
    public partial class PersonsToInitialDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Persons_ClientId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Persons_EmployeeId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Persons_OwnerId",
                table: "Estates");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "InitialData");

            migrationBuilder.AlterColumn<int>(
                name: "ServicesDirection",
                table: "InitialData",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDate",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "InitialData",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "InitialData",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LivingAddress",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PassportIssueDate",
                table: "InitialData",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PassportIssuedBy",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportSeries",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationAddress",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Owner_ApplicationDate",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner_Phone",
                table: "InitialData",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InitialData",
                table: "InitialData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InitialData_PositionId",
                table: "InitialData",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_InitialData_ClientId",
                table: "Contracts",
                column: "ClientId",
                principalTable: "InitialData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_InitialData_EmployeeId",
                table: "Contracts",
                column: "EmployeeId",
                principalTable: "InitialData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_InitialData_OwnerId",
                table: "Estates",
                column: "OwnerId",
                principalTable: "InitialData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InitialData_Positions_PositionId",
                table: "InitialData",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_InitialData_ClientId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_InitialData_EmployeeId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_InitialData_OwnerId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_InitialData_Positions_PositionId",
                table: "InitialData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InitialData",
                table: "InitialData");

            migrationBuilder.DropIndex(
                name: "IX_InitialData_PositionId",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "ApplicationDate",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "LivingAddress",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "PassportIssueDate",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "PassportIssuedBy",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "PassportSeries",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "RegistrationAddress",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "Owner_ApplicationDate",
                table: "InitialData");

            migrationBuilder.DropColumn(
                name: "Owner_Phone",
                table: "InitialData");

            migrationBuilder.RenameTable(
                name: "InitialData",
                newName: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ServicesDirection",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LivingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportIssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportIssuedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportSeries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Owner_ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Owner_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PositionId",
                table: "Persons",
                column: "PositionId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Persons_OwnerId",
                table: "Estates",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
