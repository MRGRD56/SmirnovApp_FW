using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmirnovApp.Context.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    PassportSeries = table.Column<string>(nullable: true),
                    PassportNumber = table.Column<string>(nullable: true),
                    PassportIssuedBy = table.Column<string>(nullable: true),
                    PassportIssueDate = table.Column<DateTime>(nullable: false),
                    LivingAddress = table.Column<string>(nullable: true),
                    RegistrationAddress = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ApplicationDate = table.Column<DateTime>(nullable: true),
                    Salary = table.Column<decimal>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    Owner_Phone = table.Column<string>(nullable: true),
                    Owner_ApplicationDate = table.Column<DateTime>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Estates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    Area = table.Column<int>(nullable: false),
                    FloorsCount = table.Column<int>(nullable: false),
                    Floor = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estates_Persons_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Estates_EstateTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    EstateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Persons_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Persons_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Estates_EstateId",
                        column: x => x.EstateId,
                        principalTable: "Estates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EmployeeId",
                table: "Contracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EstateId",
                table: "Contracts",
                column: "EstateId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ServiceId",
                table: "Contracts",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Estates_OwnerId",
                table: "Estates",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Estates_TypeId",
                table: "Estates",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PositionId",
                table: "Persons",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Estates");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "EstateTypes");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
