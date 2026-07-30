using CmScheme.Common.Core.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CmScheme.Common.Infrastructure.Services;

public sealed class DatabaseBackupService : IDatabaseBackupService
{
    private readonly string _connectionString;

    public DatabaseBackupService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<string> BackupAsync(CancellationToken ct = default)
    {
        string backupDir = Path.Combine(AppContext.BaseDirectory, "Backups");
        Directory.CreateDirectory(backupDir);

        string backupFile = Path.Combine(backupDir, $"CmSchemeDb_{DateTime.UtcNow:yyyyMMdd_HHmmss}.bak");

        string dbName = ExtractDatabaseName(_connectionString);
        string sql = $"BACKUP DATABASE [{dbName}] TO DISK = @path WITH FORMAT, COMPRESSION";

        await using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync(ct);

        await using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@path", backupFile);
        command.CommandTimeout = 300;

        await command.ExecuteNonQueryAsync(ct);

        return backupFile;
    }

    private static string ExtractDatabaseName(string connectionString)
    {
        SqlConnectionStringBuilder builder = new(connectionString);
        return builder.InitialCatalog;
    }
}
