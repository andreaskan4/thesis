using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

public class Facility
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string BuildingName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhotosJson { get; set; } = "[]";


    [NotMapped]
    public List<FacilityPhoto> Photos
    {
        get => string.IsNullOrEmpty(PhotosJson)
            ? new List<FacilityPhoto>()
            : JsonSerializer.Deserialize<List<FacilityPhoto>>(PhotosJson) ?? new List<FacilityPhoto>();
        set => PhotosJson = JsonSerializer.Serialize(value ?? new List<FacilityPhoto>());
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class FacilityPhoto
{
    public string Url { get; set; } = "";
    public bool IsFeatured { get; set; } = false;
    public int SortOrder { get; set; } = 0;
    public string FileName { get; set; } = "";
    public DateTime UploadedAt { get; set; } = DateTime.Now;
}