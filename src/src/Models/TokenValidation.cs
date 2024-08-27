using System.Text.Json.Serialization;

public class TokenValidation {
    
    [JsonPropertyName("Valid")]
    public bool IsValid { get; set; } = false;

    [JsonPropertyName("ReferralCode")]
    public string ReferralCode { get; set; } = string.Empty;
}