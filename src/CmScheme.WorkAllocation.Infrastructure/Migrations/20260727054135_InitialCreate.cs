using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.WorkAllocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyRecords",
                columns: table => new
                {
                    SurveyRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskProgressId = table.Column<int>(type: "int", nullable: false),
                    InternName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SurveyPersonName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PanchayatName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VillageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SurveyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SurveyStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(10,7)", precision: 10, scale: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyRecords", x => x.SurveyRecordId);
                });

            migrationBuilder.CreateTable(
                name: "TaskProgresses",
                columns: table => new
                {
                    TaskProgressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAllocationId = table.Column<int>(type: "int", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkProject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WorkDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumberOfSurveys = table.Column<int>(type: "int", nullable: false),
                    CompletedSurveys = table.Column<int>(type: "int", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompletionPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProgresses", x => x.TaskProgressId);
                });

            migrationBuilder.CreateTable(
                name: "WorkAllocations",
                columns: table => new
                {
                    WorkAllocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    WorkProjectId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    SurveysPerIntern = table.Column<int>(type: "int", nullable: false),
                    DivisionId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<int>(type: "int", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAllocations", x => x.WorkAllocationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyRecords");

            migrationBuilder.DropTable(
                name: "TaskProgresses");

            migrationBuilder.DropTable(
                name: "WorkAllocations");
        }
    }
}
