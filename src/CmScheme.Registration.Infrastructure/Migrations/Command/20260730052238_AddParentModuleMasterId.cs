using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Registration.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class AddParentModuleMasterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentModuleMasterId",
                table: "ModuleMaster",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleMaster_ParentModuleMasterId",
                table: "ModuleMaster",
                column: "ParentModuleMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleMaster_ModuleMaster_ParentModuleMasterId",
                table: "ModuleMaster",
                column: "ParentModuleMasterId",
                principalTable: "ModuleMaster",
                principalColumn: "ModuleMasterId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleMaster_ModuleMaster_ParentModuleMasterId",
                table: "ModuleMaster");

            migrationBuilder.DropIndex(
                name: "IX_ModuleMaster_ParentModuleMasterId",
                table: "ModuleMaster");

            migrationBuilder.DropColumn(
                name: "ParentModuleMasterId",
                table: "ModuleMaster");
        }
    }
}
