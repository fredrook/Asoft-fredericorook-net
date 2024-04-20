#region IMPORTS
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

public class AuthenticationController : Controller
{
    private readonly IConfiguration _configuration;

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login(string username, string password)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["JwtConfig:Issuer"],
            Audience = _configuration["JwtConfig:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }
}
