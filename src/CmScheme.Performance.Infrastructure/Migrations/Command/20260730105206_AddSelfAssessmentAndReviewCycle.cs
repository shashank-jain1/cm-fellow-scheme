using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Performance.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class AddSelfAssessmentAndReviewCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerformanceReviewCycles",
                columns: table => new
                {
                    ReviewCycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CycleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviewCycles", x => x.ReviewCycleId);
                });

            migrationBuilder.CreateTable(
                name: "SelfAssessments",
                columns: table => new
                {
                    SelfAssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    ReviewCycleId = table.Column<int>(type: "int", nullable: true),
                    Strengths = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Improvements = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GoalsAchieved = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GoalsMissed = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TrainingFeedback = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OverallRating = table.Column<int>(type: "int", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Draft")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfAssessments", x => x.SelfAssessmentId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceReviewCycles");

            migrationBuilder.DropTable(
                name: "SelfAssessments");
        }
    }
}
