using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Performance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerformanceEvaluations",
                columns: table => new
                {
                    PerformanceEvaluationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkProject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ApplicantNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApplicantName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AssignedWork = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WorkingLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TotalSurveysAssigned = table.Column<int>(type: "int", nullable: false),
                    SurveysCompleted = table.Column<int>(type: "int", nullable: false),
                    SurveysPending = table.Column<int>(type: "int", nullable: false),
                    AttendanceDays = table.Column<int>(type: "int", nullable: false),
                    LeaveDays = table.Column<int>(type: "int", nullable: false),
                    WorkingDays = table.Column<int>(type: "int", nullable: false),
                    PerformanceScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerformanceGrade = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SupervisorRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QualityScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RejectedSurveys = table.Column<int>(type: "int", nullable: false),
                    ApprovedSurveys = table.Column<int>(type: "int", nullable: false),
                    CompletionPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PerformanceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EvaluationRemarks = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    EvaluatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEvaluations", x => x.PerformanceEvaluationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceEvaluations");
        }
    }
}
