using System.Security.Claims;

public interface IReferralService{
    Task<DeepLink> GetDeepLink(ClaimsPrincipal user);
    Task<List<Referral>> GetHistory(ClaimsPrincipal user);
    Task<TokenValidation> ValidateToken(string token);
}
public class ReferralService(IReferralRepository referralRepository, IDeepLinkService deepLinkService) : IReferralService {
    public async Task<DeepLink> GetDeepLink(ClaimsPrincipal user){
        var username = user.FindFirst(ClaimTypes.Name)?.Value;
        if(string.IsNullOrWhiteSpace(username)){
            throw new UserNameNotFoundException();
        }
        var baseUrl = deepLinkService.GetBaseUrl();
        var referralCode = await referralRepository.GetReferralCode(username);
        return new DeepLink{
            Link = $"{baseUrl}?referral_code={referralCode}",
            ReferralCode = referralCode,
            BaseUrl = baseUrl
        };
    }

    public async Task<List<Referral>> GetHistory(ClaimsPrincipal user){
        var username = user.FindFirst(ClaimTypes.Name)?.Value;
        if(string.IsNullOrWhiteSpace(username)){
            throw new UserNameNotFoundException();
        }
        return await referralRepository.GetHistory(username);
    }

    public async Task<TokenValidation> ValidateToken(string token){
        var userAttribution = await deepLinkService.ValidateToken(token);

        var referralCode = userAttribution?.CustomAttributes
            .First(attr => attr.Item1 == "ReferralCode")?.Item2 ?? string.Empty;
        
        if(string.IsNullOrEmpty(referralCode)){
            return new TokenValidation(){
                IsValid = false
            };
        }
        return new TokenValidation(){
            IsValid = true,
            ReferralCode = referralCode
        };
    }
}