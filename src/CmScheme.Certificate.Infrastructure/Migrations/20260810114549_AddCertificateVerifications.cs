using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Certificate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCertificateVerifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificateVerifications",
                columns: table => new
                {
                    CertificateVerificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateId = table.Column<int>(type: "int", nullable: false),
                    CertificateNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FellowName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerificationUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    QrCodeData = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateVerifications", x => x.CertificateVerificationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertificateVerifications_CertificateNumber",
                table: "CertificateVerifications",
                column: "CertificateNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertificateVerifications");
        }
    }
}
