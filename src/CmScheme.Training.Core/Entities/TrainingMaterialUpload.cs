using System.ComponentModel.DataAnnotations;

namespace CmScheme.Training.Core.Entities;

public class TrainingMaterialUpload
{
    [Key]
    public int TrainingMaterialUploadId { get; set; }
    public int TrainingScheduleId { get; set; }
    [Required]
    [MaxLength(200)]
    public string FileName { get; set; } = null!;
    [Required]
    [MaxLength(500)]
    public string FilePath { get; set; } = null!;
    public long FileSize { get; set; }
    public int UploadedBy { get; set; }
    public DateTime UploadedOn { get; set; } = DateTime.UtcNow;
}
