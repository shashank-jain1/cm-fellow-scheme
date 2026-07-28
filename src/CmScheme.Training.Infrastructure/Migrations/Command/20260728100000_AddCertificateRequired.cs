using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Training.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class AddCertificateRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CertificateRequired",
                table: "TrainingSchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateRequired",
                table: "TrainingSchedules");
        }
    }
}
