using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InventoryWeb.Token;
public class JwtToken
{
    public string GenerateJwtToken(string secretKey, string issuer, string audience)
    {
        // 1. Define claims (payload)
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, "user123"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("role", "Admin")
    };
        // 2. Create signing credentials
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3. Create token
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds
        );
        // 4. Write token as string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
