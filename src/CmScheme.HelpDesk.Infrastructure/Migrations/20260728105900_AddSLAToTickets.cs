using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.HelpDesk.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSLAToTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SLABreached",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SLADeadline",
                table: "Tickets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SLABreached",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SLADeadline",
                table: "Tickets");
        }
    }
}
