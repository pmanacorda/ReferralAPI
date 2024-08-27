public class UserAttribution{
    public string SessionID { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public List<Tuple<string, string>> CustomAttributes { get; set; } = new(); 
}