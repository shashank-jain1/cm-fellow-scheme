using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.WorkAllocation.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class AddTaskProgressAndAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TaskProgresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FellowProgressStatus",
                table: "TaskProgresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgressNotes",
                table: "TaskProgresses",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgressPercentage",
                table: "TaskProgresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "TaskProgresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskAttachments",
                columns: table => new
                {
                    TaskAttachmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAllocationId = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachments", x => x.TaskAttachmentId);
                });

            migrationBuilder.CreateTable(
                name: "TaskDeadlines",
                columns: table => new
                {
                    TaskDeadlineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAllocationId = table.Column<int>(type: "int", nullable: false),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReminderDaysBefore = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    IsOverdue = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastReminderSentOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDeadlines", x => x.TaskDeadlineId);
                });

            migrationBuilder.CreateTable(
                name: "TaskVerifications",
                columns: table => new
                {
                    TaskVerificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAllocationId = table.Column<int>(type: "int", nullable: false),
                    VerifiedBy = table.Column<int>(type: "int", nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VerifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskVerifications", x => x.TaskVerificationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAttachments");

            migrationBuilder.DropTable(
                name: "TaskDeadlines");

            migrationBuilder.DropTable(
                name: "TaskVerifications");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TaskProgresses");

            migrationBuilder.DropColumn(
                name: "FellowProgressStatus",
                table: "TaskProgresses");

            migrationBuilder.DropColumn(
                name: "ProgressNotes",
                table: "TaskProgresses");

            migrationBuilder.DropColumn(
                name: "ProgressPercentage",
                table: "TaskProgresses");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "TaskProgresses");
        }
    }
}
