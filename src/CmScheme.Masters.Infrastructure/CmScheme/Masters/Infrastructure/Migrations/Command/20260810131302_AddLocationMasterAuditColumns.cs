using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Masters.Infrastructure.CmScheme.Masters.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class AddLocationMasterAuditColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "State",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "State",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "State",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "State",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "GramPanchayat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "GramPanchayat",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "GramPanchayat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "GramPanchayat",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Division",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Division",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Division",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Division",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "District",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "District",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "District",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "District",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Block",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Block",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Block",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Block",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "State");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "State");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "State");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "State");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "GramPanchayat");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "GramPanchayat");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "GramPanchayat");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "GramPanchayat");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Division");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Division");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Division");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Division");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "District");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "District");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "District");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "District");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Block");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Block");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Block");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Block");
        }
    }
}
