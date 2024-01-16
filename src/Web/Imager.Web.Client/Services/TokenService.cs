using System.Text.Json;

using Microsoft.JSInterop;

namespace Imager.Web.Client.Services;

public class TokenService(IConfiguration configuration, IJSRuntime jSRuntime)
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IJSRuntime _jSRuntime = jSRuntime;

    public async Task<string> GetTokenAsync()
    {
        var section = _configuration.GetSection("Google");
        var userDataKey = $"oidc.user:{section["Authority"]}:{section["ClientId"]}";
        var sessionResult = await _jSRuntime.InvokeAsync<string>("sessionStorage.getItem", userDataKey);
        var userData = JsonSerializer.Deserialize<UserData>(sessionResult);
        return userData!.id_token;
    }

    class UserData
    {
        public string id_token { get; set; } = null!;
    }
}