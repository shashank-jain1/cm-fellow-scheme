using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmScheme.Masters.Infrastructure.Migrations.Command
{
    /// <inheritdoc />
    public partial class FixDepartmentTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Department' AND type = 'U')
                    EXEC sp_rename 'Department', 'Departments';
                ELSE IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Departments' AND type = 'U')
                    CREATE TABLE [Departments] (
                        [DepartmentId] INT NOT NULL IDENTITY(1,1),
                        [DepartmentName] NVARCHAR(150) NOT NULL,
                        [DepartmentCode] NVARCHAR(50) NULL,
                        [IsActive] BIT NOT NULL DEFAULT 1,
                        [CreatedOn] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                        [CreatedBy] INT NULL,
                        [ModifiedOn] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
                        [ModifiedBy] INT NULL,
                        CONSTRAINT [PK_Departments] PRIMARY KEY ([DepartmentId])
                    );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Departments' AND type = 'U')
                    EXEC sp_rename 'Departments', 'Department';
            ");
        }
    }
}
