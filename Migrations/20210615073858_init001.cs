using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project5.Migrations
{
    public partial class init001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Number = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    Address = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false),
                    PersonCarsId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonCars",
                columns: table => new
                {
                    CarId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCars", x => new { x.CarId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonCars_Car",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonCars_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Registrator",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    GerNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrator_Person",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateTmeOfAccident = table.Column<DateTime>(type: "datetime", nullable: false),
                    Value = table.Column<decimal>(type: "money", nullable: false),
                    RegistratorId = table.Column<long>(type: "bigint", nullable: false),
                    CarId = table.Column<long>(type: "bigint", nullable: true),
                    DriverId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fine_Car",
                        column: x => x.CarId,
                        principalTable: "Car",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fine_Person",
                        column: x => x.DriverId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fine_Registrator",
                        column: x => x.RegistratorId,
                        principalTable: "Registrator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fine_CarId",
                table: "Fine",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Fine_DriverId",
                table: "Fine",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Fine_RegistratorId",
                table: "Fine",
                column: "RegistratorId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCars_PersonId",
                table: "PersonCars",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrator_PersonId",
                table: "Registrator",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fine");

            migrationBuilder.DropTable(
                name: "PersonCars");

            migrationBuilder.DropTable(
                name: "Registrator");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
