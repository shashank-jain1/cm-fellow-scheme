namespace CmScheme.Dashboard.Core.Dtos;

public sealed record AdminDashboardDto(
    int TotalRegisteredUsers,
    int TotalProjects,
    int TotalSurveysCompleted,
    int TotalSurveysPending,
    int TotalTicketsOpen,
    decimal OverallAttendancePercentage,
    decimal OverallSurveyCompletionPercentage);

public sealed record CoordinatorDashboardDto(
    int TeamSize,
    int ActiveProjects,
    int PendingTasks,
    int CompletedSurveys,
    int PendingSurveys,
    decimal TeamAttendancePercentage);

public sealed record ProjectProgressDto(
    string ProjectName,
    decimal CompletionPercentage,
    int TotalSurveys,
    int CompletedSurveys);
