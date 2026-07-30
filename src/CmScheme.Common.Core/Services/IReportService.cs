namespace CmScheme.Common.Core.Services;

public interface IReportService
{
    Task<byte[]> GenerateAttendanceReportAsync(int userId, int month, int year, CancellationToken ct = default);
    Task<byte[]> GeneratePerformanceReportAsync(int userId, CancellationToken ct = default);
    Task<byte[]> GenerateLeaveReportAsync(int userId, CancellationToken ct = default);
    Task<byte[]> GenerateTrainingReportAsync(int trainingId, CancellationToken ct = default);
}
