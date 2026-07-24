namespace CmScheme.WorkAllocation.Core.Entities;

public class SurveyRecord
{
    public int SurveyRecordId { get; set; }
    public int TaskProgressId { get; set; }
    public string InternName { get; set; } = null!;
    public string SurveyPersonName { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public string PanchayatName { get; set; } = null!;
    public string VillageName { get; set; } = null!;
    public DateTime SurveyDate { get; set; }
    public string SurveyStatus { get; set; } = null!;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
}
