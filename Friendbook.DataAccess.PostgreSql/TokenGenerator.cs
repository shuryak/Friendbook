using System.Security.Cryptography;

namespace Friendbook.DataAccess.PostgreSql;

public static class TokenGenerator
{
    public static string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];

        using RandomNumberGenerator generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);
        
        return Convert.ToBase64String(randomNumber);
    }
}
