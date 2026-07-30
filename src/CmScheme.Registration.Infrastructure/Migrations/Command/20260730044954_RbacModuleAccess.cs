using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Registration.Infrastructure.CmScheme.Registration.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class RbacModuleAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModuleMaster",
                columns: table => new
                {
                    ModuleMasterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleMaster", x => x.ModuleMasterId);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    RoleLookupId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRole_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModuleAccessAuditLog",
                columns: table => new
                {
                    ModuleAccessAuditLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserModuleAccessId = table.Column<int>(type: "int", nullable: false),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    ModuleMasterId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PerformedBy = table.Column<int>(type: "int", nullable: false),
                    PerformedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleAccessAuditLog", x => x.ModuleAccessAuditLogId);
                    table.ForeignKey(
                        name: "FK_ModuleAccessAuditLog_ModuleMaster_ModuleMasterId",
                        column: x => x.ModuleMasterId,
                        principalTable: "ModuleMaster",
                        principalColumn: "ModuleMasterId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleAccessAuditLog_UserAccount_PerformedBy",
                        column: x => x.PerformedBy,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleAccessAuditLog_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserModuleAccess",
                columns: table => new
                {
                    UserModuleAccessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountId = table.Column<int>(type: "int", nullable: false),
                    ModuleMasterId = table.Column<int>(type: "int", nullable: false),
                    CanRead = table.Column<bool>(type: "bit", nullable: false),
                    CanWrite = table.Column<bool>(type: "bit", nullable: false),
                    CanApprove = table.Column<bool>(type: "bit", nullable: false),
                    CanExport = table.Column<bool>(type: "bit", nullable: false),
                    DivisionId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    BlockId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModuleAccess", x => x.UserModuleAccessId);
                    table.ForeignKey(
                        name: "FK_UserModuleAccess_ModuleMaster_ModuleMasterId",
                        column: x => x.ModuleMasterId,
                        principalTable: "ModuleMaster",
                        principalColumn: "ModuleMasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserModuleAccess_UserAccount_UserAccountId",
                        column: x => x.UserAccountId,
                        principalTable: "UserAccount",
                        principalColumn: "UserAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAccessAuditLog_ModuleMasterId",
                table: "ModuleAccessAuditLog",
                column: "ModuleMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAccessAuditLog_PerformedBy",
                table: "ModuleAccessAuditLog",
                column: "PerformedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAccessAuditLog_PerformedOn",
                table: "ModuleAccessAuditLog",
                column: "PerformedOn");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAccessAuditLog_UserAccountId",
                table: "ModuleAccessAuditLog",
                column: "UserAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleMaster_ModuleCode",
                table: "ModuleMaster",
                column: "ModuleCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleAccess_ModuleMasterId",
                table: "UserModuleAccess",
                column: "ModuleMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModuleAccess_UserAccountId_ModuleMasterId_DivisionId_DistrictId_BlockId",
                table: "UserModuleAccess",
                columns: new[] { "UserAccountId", "ModuleMasterId", "DivisionId", "DistrictId", "BlockId" },
                unique: true,
                filter: "[DivisionId] IS NOT NULL AND [DistrictId] IS NOT NULL AND [BlockId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserAccountId_RoleLookupId",
                table: "UserRole",
                columns: new[] { "UserAccountId", "RoleLookupId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleAccessAuditLog");

            migrationBuilder.DropTable(
                name: "UserModuleAccess");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "ModuleMaster");
        }
    }
}
