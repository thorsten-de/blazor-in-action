using System.Security.Claims;

namespace BlazingTrails.Client.Features.Home;

public static class AccessRules
{
    public static bool IsEditorOf(this ClaimsPrincipal user, Trail trail) =>
        trail.Owner.Equals(user.Identity?.Name, StringComparison.OrdinalIgnoreCase) || user.IsInRole("Administrator");
}