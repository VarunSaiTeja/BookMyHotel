using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyHotel.Migrations
{
    /// <inheritdoc />
    public partial class Dc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryChargePerKM",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryChargePerKM",
                table: "Hotels");
        }
    }
}
