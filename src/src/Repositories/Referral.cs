using System.Collections.Frozen;

public interface IReferralRepository{
    Task<List<Referral>> GetHistory(string username);
    Task<string> GetReferralCode(string username);
}

public class ReferralRespository : IReferralRepository
{
    private readonly Dictionary<string, string> _ReferralCodes = new Dictionary<string, string>{
        {"PMANACORDA", "XY7G4D"}
    };
    private readonly Dictionary<string, List<Referral>> _ReferralHistory = new Dictionary<string, List<Referral>>{
        {"PMANACORDA", new List<Referral>(){
            new Referral(){Name="Helen G.", Date=DateTime.Today}
        }}
    };
    public async Task<List<Referral>> GetHistory(string username)
    {
        await Task.Delay(100);
        if(_ReferralHistory.TryGetValue(username.ToUpper(), out var referrals)){
            return referrals;
        }
        return new List<Referral>();
    }

    public async Task<string> GetReferralCode(string username)
    {
        await Task.Delay(100);
        if(_ReferralCodes.TryGetValue(username.ToUpper(), out var code)){
            return code;
        }
        throw new UserNameNotFoundException();
    }
}