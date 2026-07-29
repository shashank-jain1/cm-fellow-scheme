using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Performance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PendingFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PerformanceEvaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PerformanceEvaluations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "PerformanceEvaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PerformanceEvaluations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReviewLevel",
                table: "PerformanceEvaluations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Draft");

            migrationBuilder.AddColumn<string>(
                name: "ReviewStatus",
                table: "PerformanceEvaluations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Draft");

            migrationBuilder.CreateTable(
                name: "PerformanceReviewHistories",
                columns: table => new
                {
                    PerformanceReviewHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceEvaluationId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PreviousLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NewLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PreviousStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    PerformedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviewHistories", x => x.PerformanceReviewHistoryId);
                    table.ForeignKey(
                        name: "FK_PerformanceReviewHistories_PerformanceEvaluations_PerformanceEvaluationId",
                        column: x => x.PerformanceEvaluationId,
                        principalTable: "PerformanceEvaluations",
                        principalColumn: "PerformanceEvaluationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviewHistories_PerformanceEvaluationId",
                table: "PerformanceReviewHistories",
                column: "PerformanceEvaluationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceReviewHistories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PerformanceEvaluations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PerformanceEvaluations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PerformanceEvaluations");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PerformanceEvaluations");

            migrationBuilder.DropColumn(
                name: "ReviewLevel",
                table: "PerformanceEvaluations");

            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "PerformanceEvaluations");
        }
    }
}
