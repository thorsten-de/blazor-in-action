using System;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace BlazingTrails.Client.Features.Auth;

public class CustomUserFactory<TAccount> : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public CustomUserFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
    {
    }

    /// <summary>
    /// We override CreateUserAsync to split the role claim with its multiple values into many role claims with a single value.
    /// </summary>
    /// <param name="account"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var initialUser = await base.CreateUserAsync(account, options);

        if (initialUser?.Identity?.IsAuthenticated ?? false)
        {
            var userIdentity = (ClaimsIdentity)initialUser.Identity;

            account.AdditionalProperties.TryGetValue(ClaimTypes.Role, out var roleClaimValue);

            if (roleClaimValue is not null && roleClaimValue is JsonElement element && element.ValueKind == JsonValueKind.Array)
            {
                userIdentity.RemoveClaim(userIdentity.FindFirst(ClaimTypes.Role));
                var claims = element.EnumerateArray()
                    .Select(c => new Claim(ClaimTypes.Role, c.ToString()));

                userIdentity.AddClaims(claims);
            }
        }

        return initialUser ?? new();
    }
}
