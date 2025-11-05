using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBB_ApplicationDashboard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondaryBusinessTypesInAccreditation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondaryBusinessTypes",
                table: "Accreditations",
                type: "jsonb",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "SecondaryBusinessTypes", table: "Accreditations");
        }
    }
}
