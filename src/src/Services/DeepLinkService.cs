public interface IDeepLinkService {
    string GetBaseUrl();
    Task<UserAttribution> ValidateToken(string token);
}
public class DeepLinkService(IConfiguration configuration) : IDeepLinkService
{
    private string _baseUrl = string.Empty;
    private Dictionary<string, UserAttribution> _remoteService = new Dictionary<string, UserAttribution>{
        {"JwbWFuYWNvcmRhI", new(){
            Link = "https://cartoncaps.link/abfilefa90p?referral_code=XY7G4D",
            SessionID = "Session0",
            CustomAttributes = [
                Tuple.Create("ReferralCode", "XY7G4D")
            ]
        }}
    };
    public string GetBaseUrl()
    {
        if(string.IsNullOrWhiteSpace(_baseUrl)){
            _baseUrl = configuration["DEEP_LINK_URL"] ?? throw new ConfigurationNotFoundException();
        }
        return _baseUrl;
    }

    public async Task<UserAttribution> ValidateToken(string token)
    {
        await Task.Delay(100);
        if(_remoteService.TryGetValue(token, out var attribution)){
            return attribution;
        }
        return default!;
    }
}