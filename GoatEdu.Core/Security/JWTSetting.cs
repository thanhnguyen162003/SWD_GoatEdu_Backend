using Microsoft.Extensions.Configuration;

namespace GoatEdu.Core.Interfaces.Security;

public class JWTSetting
{
    public string? SecurityKey { get; set; }
    public double? TokenExpiry { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }

    public JWTSetting()
    {
        GetSettingConfig();
    }

    private void GetSettingConfig()
    {
        IConfiguration config = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

            .AddJsonFile("appsettings.json", true, true)

            .Build();

        this.SecurityKey = config["JWTSetting:SecurityKey"];
        this.Issuer = config["JWTSetting:Issuer"];
        this.Audience = config["JWTSetting:Audience"];
        this.TokenExpiry = Convert.ToDouble(config["JWTSetting:TokenExpiry"]);

    }
}