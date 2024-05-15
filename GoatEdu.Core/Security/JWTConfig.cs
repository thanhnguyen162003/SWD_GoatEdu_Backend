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

    public string? GenerateToken(LoginDtoRequest userDTO)
    {
        var user = _userRepository.GetUserByUsername(userDTO.Username);
        if (user == null)
        {
            return "Error! Unauthorized.";
        }
        var tokenhandler = new JwtSecurityTokenHandler();
        var tokenkey = Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userDTO.Username),
                
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _jwtSetting.Issuer,
            Audience = _jwtSetting.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenhandler.CreateToken(tokenDescriptor);
        return tokenhandler.WriteToken(token);
    }
}