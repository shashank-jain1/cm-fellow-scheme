using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.WorkAllocation.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class AddTaskDependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskDependencies",
                columns: table => new
                {
                    TaskDependencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAllocationId = table.Column<int>(type: "int", nullable: false),
                    DependsOnWorkAllocationId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDependencies", x => x.TaskDependencyId);
                    table.ForeignKey(
                        name: "FK_TaskDependencies_WorkAllocations_DependsOnWorkAllocationId",
                        column: x => x.DependsOnWorkAllocationId,
                        principalTable: "WorkAllocations",
                        principalColumn: "WorkAllocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskDependencies_WorkAllocations_WorkAllocationId",
                        column: x => x.WorkAllocationId,
                        principalTable: "WorkAllocations",
                        principalColumn: "WorkAllocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDependencies_DependsOnWorkAllocationId",
                table: "TaskDependencies",
                column: "DependsOnWorkAllocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDependencies_WorkAllocationId",
                table: "TaskDependencies",
                column: "WorkAllocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskDependencies");
        }
    }
}
