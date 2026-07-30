using System.ComponentModel.DataAnnotations;

namespace CmScheme.Training.Core.Entities;

public class TrainingMaterial
{
    [Key]
    public int TrainingMaterialId { get; set; }
    public int TrainingScheduleId { get; set; }
    [Required]
    [MaxLength(200)]
    public string MaterialName { get; set; } = null!;
    [Required]
    [MaxLength(500)]
    public string FilePath { get; set; } = null!;
    public long FileSize { get; set; }
    [MaxLength(100)]
    public string? ContentType { get; set; }
    public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}
