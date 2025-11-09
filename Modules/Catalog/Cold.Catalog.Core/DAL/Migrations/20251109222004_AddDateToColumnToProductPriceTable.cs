using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cold.Catalog.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToColumnToProductPriceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Class",
                schema: "catalog",
                table: "ProductsPrices",
                newName: "ClassType");

            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "catalog",
                table: "ProductsPrices",
                newName: "DateFrom");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTo",
                schema: "catalog",
                table: "ProductsPrices",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTo",
                schema: "catalog",
                table: "ProductsPrices");

            migrationBuilder.RenameColumn(
                name: "ClassType",
                schema: "catalog",
                table: "ProductsPrices",
                newName: "Class");

            migrationBuilder.RenameColumn(
                name: "DateFrom",
                schema: "catalog",
                table: "ProductsPrices",
                newName: "Date");
        }
    }
}
