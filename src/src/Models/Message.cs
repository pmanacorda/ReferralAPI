using System.Text.Json.Serialization;

public enum MESSAGE_CODE{
    UNKNOWN = 0,
    DATA_NOT_FOUND = 1,
}

public class Message {

    [JsonPropertyName("Code")]
    public MESSAGE_CODE Code { get; set; } = MESSAGE_CODE.UNKNOWN;

    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;
}

public static class GlobalMessages
{
    public static readonly Message INTERNAL_SERVER_ERROR = new Message()
    {
        Code = MESSAGE_CODE.UNKNOWN,
        Description = "An unexpected error occurred"
    };

    public static readonly Message NO_REFERRALS_FOUND = new Message()
    {
        Code = MESSAGE_CODE.DATA_NOT_FOUND,
        Description = "The user does not have any referrals"
    };
}