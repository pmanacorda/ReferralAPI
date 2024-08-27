using System.Text.Json.Serialization;

public class ApiResponse<T> where T : class, new() {

    [JsonPropertyName("Content")]
    public T Content { get; set; } = new T();

    [JsonPropertyName("Messages")]
    public List<Message> Messages { get; set; } = new List<Message>();
}