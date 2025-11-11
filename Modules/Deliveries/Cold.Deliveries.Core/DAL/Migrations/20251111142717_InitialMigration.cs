using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cold.Deliveries.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "deliveries");

            migrationBuilder.CreateTable(
                name: "Deliveries",
                schema: "deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TotalValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsInvoiced = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportStatuses",
                schema: "deliveries",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryPhotos",
                schema: "deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryPhotos_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalSchema: "deliveries",
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryProducts",
                schema: "deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryProducts_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalSchema: "deliveries",
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportRequests",
                schema: "deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uuid", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransportStatusId = table.Column<short>(type: "smallint", nullable: false),
                    RequestDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ScheduledPickupDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ActualPickupDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ActualDeliveryDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportRequests_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalSchema: "deliveries",
                        principalTable: "Deliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TransportRequests_TransportStatuses_TransportStatusId",
                        column: x => x.TransportStatusId,
                        principalSchema: "deliveries",
                        principalTable: "TransportStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "deliveries",
                table: "TransportStatuses",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { (short)1, "Do realizacji", "ToRealize" },
                    { (short)2, "W drodze do dostawcy", "OnWayToSupplier" },
                    { (short)3, "U dostawcy", "AtSupplier" },
                    { (short)4, "W drodze do chłodni", "OnWayToColdStorage" },
                    { (short)5, "W chłodni", "InColdStorage" },
                    { (short)6, "Anulowano", "Cancelled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryNumber",
                schema: "deliveries",
                table: "Deliveries",
                column: "DeliveryNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPhotos_DeliveryId",
                schema: "deliveries",
                table: "DeliveryPhotos",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryProducts_DeliveryId",
                schema: "deliveries",
                table: "DeliveryProducts",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportRequests_DeliveryId",
                schema: "deliveries",
                table: "TransportRequests",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportRequests_TransportStatusId",
                schema: "deliveries",
                table: "TransportRequests",
                column: "TransportStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryPhotos",
                schema: "deliveries");

            migrationBuilder.DropTable(
                name: "DeliveryProducts",
                schema: "deliveries");

            migrationBuilder.DropTable(
                name: "TransportRequests",
                schema: "deliveries");

            migrationBuilder.DropTable(
                name: "Deliveries",
                schema: "deliveries");

            migrationBuilder.DropTable(
                name: "TransportStatuses",
                schema: "deliveries");
        }
    }
}
