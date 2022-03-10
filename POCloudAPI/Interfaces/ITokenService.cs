using POCloudAPI.Entities;

namespace POCloudAPI.JWTokens
{
    public interface ITokenService
    {
        string createToken(APIUser user);
    }
}
