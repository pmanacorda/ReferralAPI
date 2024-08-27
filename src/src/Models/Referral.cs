using System.Text.Json.Serialization;

public class Referral{
    
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("Date")]
    public DateTime Date { get; set; } = default;
}