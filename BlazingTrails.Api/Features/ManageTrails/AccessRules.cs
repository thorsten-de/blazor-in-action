using System.Security.Claims;
using BlazingTrails.Persistence.Model;

namespace BlazingTrails.Api.Features.ManageTrails;

public static class AccessRules
{
    public static bool IsEditorOf(this ClaimsPrincipal user, Trail trail) =>
        trail.Owner.Equals(user.Identity?.Name, StringComparison.OrdinalIgnoreCase);
}