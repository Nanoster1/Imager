using System.Security.Claims;

using Google.Apis.Auth;

using Imager.ImageResizerAggregator.Server.Authentication.Constants;

using Microsoft.IdentityModel.Tokens;

namespace Imager.ImageResizerAggregator.Server.Authentication.Validators;

public class GoogleTokenHandler : TokenHandler
{
    public override async Task<TokenValidationResult> ValidateTokenAsync(string token, TokenValidationParameters validationParameters)
    {
        try
        {
            var claimsIdentity = new ClaimsIdentity();
            var payload = await GoogleJsonWebSignature.ValidateAsync(token); //TODO Audience
            var userId = payload.Subject;

            claimsIdentity.AddClaim(new(UserClaimTypes.Id, userId));

            return new TokenValidationResult()
            {
                ClaimsIdentity = claimsIdentity,
                IsValid = true
            };
        }
        catch (Exception ex)
        {
            return new TokenValidationResult
            {
                IsValid = false,
                Exception = ex
            };
        }
    }
}