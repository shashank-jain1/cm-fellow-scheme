using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Registration.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class AddPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExitInterviews",
                columns: table => new
                {
                    ExitInterviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    OverallExperience = table.Column<int>(type: "int", nullable: false),
                    WorkEnvironment = table.Column<int>(type: "int", nullable: false),
                    LearningOpportunities = table.Column<int>(type: "int", nullable: false),
                    TeamCollaboration = table.Column<int>(type: "int", nullable: false),
                    ImprovementSuggestions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WhatWorkedWell = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WouldRecommend = table.Column<bool>(type: "bit", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExitInterviews", x => x.ExitInterviewId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExitInterviews");
        }
    }
}
