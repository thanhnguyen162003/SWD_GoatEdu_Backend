using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.Security;
using GoatEdu.Core.Interfaces.UserInterfaces;
using Microsoft.IdentityModel.Tokens;


public class JWTConfig : JWTGenerator
{
    private readonly JWTSetting _jwtSetting;
    private readonly IUserRepository _userRepository;

    public JWTConfig(JWTSetting jwtSetting, IUserRepository userRepository)
    {
        _jwtSetting = jwtSetting;
        _userRepository = userRepository;
    }

    public JWTConfig(IUserRepository userRepository)
    {
        _jwtSetting = new JWTSetting();
        _userRepository = userRepository;
    }

    public async Task<string> GenerateToken(LoginDtoRequest userDTO)
    {
        var user = await _userRepository.GetUserByUsername(userDTO.Username);
        if (user == null)
        {
            return "Error! Unauthorized.";
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userDTO.Username),
                new Claim("UserId", user.Id.ToString()),
                new Claim("RoleId",user.RoleId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
                
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _jwtSetting.Issuer,
            Audience = _jwtSetting.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}