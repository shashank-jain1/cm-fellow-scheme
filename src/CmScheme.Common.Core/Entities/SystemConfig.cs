namespace CmScheme.Common.Core.Entities;

public class SystemConfig
{
    public int SystemConfigId { get; set; }
    public string ConfigKey { get; set; } = null!;
    public string ConfigValue { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
    public string? ModifiedBy { get; set; }
}
