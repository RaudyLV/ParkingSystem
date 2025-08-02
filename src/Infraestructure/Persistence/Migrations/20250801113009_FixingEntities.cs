using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "ParkingSlots",
                type: "decimal(18,2)",
                nullable: false,
                defaultValueSql: "100.00",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValueSql: "0.00");

            migrationBuilder.AddColumn<string>(
                name: "VehicleType",
                table: "ParkingSlots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "ParkingSessions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValueSql: "0.00",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "VehiclePlate",
                table: "ParkingSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "ParkingSlots");

            migrationBuilder.DropColumn(
                name: "VehiclePlate",
                table: "ParkingSessions");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "ParkingSlots",
                type: "decimal(18,2)",
                nullable: false,
                defaultValueSql: "0.00",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValueSql: "100.00");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "ParkingSessions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValueSql: "0.00");
        }
    }
}
