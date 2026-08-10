using CmScheme.Common.Infrastructure.Services;

namespace CmScheme.Tests.Common;

public class StatusWorkflowServiceTests
{
    private readonly StatusWorkflowService _service = new();

    [Theory]
    [InlineData("Submitted", "Under Review")]
    [InlineData("Submitted", "Rejected")]
    [InlineData("Under Review", "Approved")]
    [InlineData("Under Review", "Rejected")]
    [InlineData("Under Review", "Additional Info Required")]
    [InlineData("Additional Info Required", "Under Review")]
    [InlineData("Additional Info Required", "Rejected")]
    public void CanTransition_Allows_Valid_Transitions(string current, string target)
    {
        Assert.True(_service.CanTransition(current, target));
    }

    [Theory]
    [InlineData("Submitted", "Approved")]
    [InlineData("Submitted", "Additional Info Required")]
    [InlineData("Under Review", "Submitted")]
    [InlineData("Additional Info Required", "Approved")]
    [InlineData("Approved", "Rejected")]
    [InlineData("Rejected", "Under Review")]
    public void CanTransition_Rejects_Invalid_Transitions(string current, string target)
    {
        Assert.False(_service.CanTransition(current, target));
    }

    [Theory]
    [InlineData("Submitted", "Submitted")]
    [InlineData("Under Review", "Under Review")]
    public void CanTransition_Rejects_Noop_Transitions(string current, string target)
    {
        Assert.False(_service.CanTransition(current, target));
    }

    [Fact]
    public void GetAllowedTransitions_Submitted_Returns_UnderReview_And_Rejected()
    {
        var transitions = _service.GetAllowedTransitions("Submitted");
        Assert.Equal(["Under Review", "Rejected"], transitions);
    }

    [Fact]
    public void GetAllowedTransitions_UnderReview_Returns_Expected_Set()
    {
        var transitions = _service.GetAllowedTransitions("Under Review");
        Assert.Equal(["Approved", "Rejected", "Additional Info Required"], transitions);
    }

    [Fact]
    public void GetAllowedTransitions_Approved_Is_Empty()
    {
        Assert.Empty(_service.GetAllowedTransitions("Approved"));
    }

    [Fact]
    public void GetAllowedTransitions_Rejected_Is_Empty()
    {
        Assert.Empty(_service.GetAllowedTransitions("Rejected"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("Unknown")]
    [InlineData("Pending")]
    public void GetAllowedTransitions_Unknown_Status_Returns_Empty(string status)
    {
        Assert.Empty(_service.GetAllowedTransitions(status));
    }

    [Fact]
    public void CanTransition_Unknown_Status_Is_False()
    {
        Assert.False(_service.CanTransition("Unknown", "Approved"));
    }
}