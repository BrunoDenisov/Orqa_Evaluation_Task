using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    
    private const string SecretKey = "tsK+SFT/VdTNaqMYIAOLSfByP9RbQPrN0bRKZ5ndX01dxbjX78S8VE0KJHYGxwz7";

    public static string GenerateToken(string username, string role, int expirationMinutes = 60)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "Orqa_Db_DataBase",
            audience: "Orqa_DbApp",
            claims: new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            },
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials);

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public static string ExtractRoleFromAccessToken(string accessToken)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            var roleClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role); // Extracting the role claim

            return roleClaim?.Value;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to extract role from access token.", ex);
        }
    }

    public static string ExtractUsernameFromAccessToken(string accessToken)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;

            var usernameClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name); // Extracting the username claim

            return usernameClaim?.Value;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to extract username from access token.", ex);
        }
    }

}
