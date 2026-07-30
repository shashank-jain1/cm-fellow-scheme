using System.ComponentModel.DataAnnotations;

namespace CmScheme.Registration.Core.Entities;

public class ExitInterview
{
    [Key]
    public int ExitInterviewId { get; set; }
    public int UserAccountId { get; set; }
    public int OverallExperience { get; set; }
    public int WorkEnvironment { get; set; }
    public int LearningOpportunities { get; set; }
    public int TeamCollaboration { get; set; }
    [MaxLength(1000)]
    public string? ImprovementSuggestions { get; set; }
    [MaxLength(1000)]
    public string? WhatWorkedWell { get; set; }
    public bool WouldRecommend { get; set; }
    public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
}
