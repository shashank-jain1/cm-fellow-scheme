using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Registration.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class AddTrainingEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingEnrollment",
                columns: table => new
                {
                    TrainingEnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingScheduleId = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AttendanceMarked = table.Column<bool>(type: "bit", nullable: false),
                    CertificateIssued = table.Column<bool>(type: "bit", nullable: false),
                    EnrolledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEnrollment", x => x.TrainingEnrollmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEnrollment_TrainingScheduleId_UserAccountId",
                table: "TrainingEnrollment",
                columns: new[] { "TrainingScheduleId", "UserAccountId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingEnrollment");
        }
    }
}
