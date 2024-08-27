using System.Text.Json.Serialization;

public class DeepLink{
    
    [JsonPropertyName("Link")]
    public string Link { get; set; } = string.Empty;

    [JsonPropertyName("Code")]
    public string ReferralCode { get; set; } = string.Empty;

    [JsonPropertyName("Url")]
    public string BaseUrl { get; set; } = string.Empty;
}