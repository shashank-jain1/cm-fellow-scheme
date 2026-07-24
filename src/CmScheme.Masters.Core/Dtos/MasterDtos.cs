namespace CmScheme.Masters.Core.Dtos;

public sealed record StateDto(int StateId, string StateName, string StateCode, string? StateShortName, int? DisplayOrder, bool IsActive);
public sealed record DivisionDto(int DivisionId, int StateId, string DivisionName, string? DivisionCode, bool IsActive);
public sealed record DistrictDto(int DistrictId, int DivisionId, string DistrictName, string? DistrictCode, bool IsActive);
public sealed record BlockDto(int BlockId, int DistrictId, string BlockName, string? BlockCode, bool IsActive);
public sealed record GramPanchayatDto(int GramPanchayatId, int BlockId, string GramPanchayatName, string? GPCode, bool IsActive);
public sealed record ProjectDto(int ProjectId, string ProjectName, string ProjectCode, string? ProjectDescription, string DepartmentName, DateTime StartDate, DateTime EndDate, string ProjectIncharge, decimal? BudgetAmount, bool IsActive);
public sealed record WorkDto(int WorkId, int ProjectId, string WorkName, string? WorkDescription, string Priority, DateTime StartDate, DateTime EndDate, string AssignedTo, string? Remarks, bool IsActive);
