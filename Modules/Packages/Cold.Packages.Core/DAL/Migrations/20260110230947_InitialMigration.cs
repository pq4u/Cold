using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cold.Packages.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "packages");

            migrationBuilder.CreateTable(
                name: "PackageRentals",
                schema: "packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageRentals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                schema: "packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageRentalItems",
                schema: "packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageRentalId = table.Column<Guid>(type: "uuid", nullable: false),
                    PackageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageRentalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageRentalItems_PackageRentals_PackageRentalId",
                        column: x => x.PackageRentalId,
                        principalSchema: "packages",
                        principalTable: "PackageRentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageRentalItems_Packages_PackageId",
                        column: x => x.PackageId,
                        principalSchema: "packages",
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackageRentalItems_PackageId",
                schema: "packages",
                table: "PackageRentalItems",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageRentalItems_PackageRentalId",
                schema: "packages",
                table: "PackageRentalItems",
                column: "PackageRentalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageRentalItems",
                schema: "packages");

            migrationBuilder.DropTable(
                name: "PackageRentals",
                schema: "packages");

            migrationBuilder.DropTable(
                name: "Packages",
                schema: "packages");
        }
    }
}
