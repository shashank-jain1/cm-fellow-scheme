using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Training.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingParticipants",
                columns: table => new
                {
                    TrainingParticipantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingScheduleId = table.Column<int>(type: "int", nullable: false),
                    ParticipantUserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingParticipants", x => x.TrainingParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSchedules",
                columns: table => new
                {
                    TrainingScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    WorkProjectId = table.Column<int>(type: "int", nullable: false),
                    ActivityTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrainingTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MeetingTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrainingCategory = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TrainingDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MeetingAgenda = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MeetingDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ConductPersonId = table.Column<int>(type: "int", nullable: true),
                    CoordinatorId = table.Column<int>(type: "int", nullable: true),
                    TrainerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TrainerMobile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AttendanceRequired = table.Column<bool>(type: "bit", nullable: false),
                    MOMRequired = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MaterialPath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSchedules", x => x.TrainingScheduleId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingParticipants");

            migrationBuilder.DropTable(
                name: "TrainingSchedules");
        }
    }
}
