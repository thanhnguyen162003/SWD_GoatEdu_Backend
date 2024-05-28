using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.RoleInterfaces;
using GoatEdu.Core.Interfaces.Security;
using GoatEdu.Core.Interfaces.UserInterfaces;
using GoatEdu.Core.Security;
using Microsoft.IdentityModel.Tokens;


public class JWTConfig : JWTGenerator
{
    private readonly JWTSetting _jwtSetting;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;


    public JWTConfig(JWTSetting jwtSetting, IUserRepository userRepository,IRoleRepository roleRepository)
    {
        _jwtSetting = jwtSetting;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
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
    public async Task<string> GenerateTokenGoogle(string email)
    {
        var user = await _userRepository.GetUserByUsername(email);
        if (user == null)
        {
            throw new NullReferenceException("User not found");
        }

        if (user.Role.RoleName == null)
        {
            throw new NullReferenceException("User role not found");
        }

        if (_jwtSetting == null)
        {
            throw new NullReferenceException("JWT settings are not configured properly");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id.ToString()),
                new Claim("RoleId", user.RoleId.ToString()),
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