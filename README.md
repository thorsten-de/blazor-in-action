# Blazor in Action

In this repo, I follow the book [Blazor in Action](https://www.manning.com/books/blazor-in-action) by Chris Santy. I really enjoy using my Manning subscription to get my feet wet with stuff I have not the time to use on a regular basis on my job.

## Differences with .NET 10

There are likely to be some minor (and hopefully few major) differences to the book version when using .NET 10 to implement the examples. I'd like to have them documented here.

### Colocated JS Files Bug in .NET 10 Blazor WebAssembly

**Problem:** When using colocated JavaScript files (JS file next to the `.razor` component) in .NET 10 Blazor WebAssembly, the build process _generates a content hash for the import path at runtime_, but _does not rename the physical file_ accordingly.

**Symptom:** The browser tries to load the file from a hashed path (e.g., `RouteMap.xaoeylw2gv.razor.js`), but the file only exists without the hash (e.g., `RouteMap.razor.js`).

**Workaround:** Add a query parameter to the import path:

```csharp
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        var modulePath = "./Features/Map/RouteMap.razor.js?v=1";
        _routeMapModule = await JS.InvokeAsync<IJSObjectReference>("import", modulePath);
    }
}
```

**Status:** This bug should be fixed in a future .NET 10 patch. The workaround can be removed once the fix is available.

### Appendix: Adding an ASP.NET Core Web API

When we create the webapi project, we need to tell that we want to use controllers,
as the default has changed to start with a minimal api: `dotnet new webapi --use-controllers ...`

Configure the Project to support a Blazor WebAssembly client: <https://www.nuget.org/packages/Microsoft.AspNetCore.Components.WebAssembly.Server>

## EditContext

We have access to the EditContext and can:

- perform actions manually like triggering validation
- hook into events like `OnFieldChanged`
- plug in a custom CSS class provider


## Setting up Auth0

We setup Auth0 by:
- creating a free account, and within we 
- create a new single-page web application "Blazing Trails Client". We set the Allowed Callback URLs to https://localhost:7085/authentication/login-callback
- create a new api "Blazing Trails API" with identifier "https://blazingtrails.com/api"

### Customize the claims

We customize the returned claims by adding a Trigger action for Post Login:
```js
exports.onExecutePostLogin = async (event, api) => {    
    api.accessToken.setCustomClaim(`http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name`, event.user.email);
        
    api.idToken.setCustomClaim(`http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress`, event.user.email);
};
```

### Setting the Audience in the client

Following the book, we got an **opaque token**. These are in a proprietary format and the recipient must call the server to get the information. To get a self-contained **JWT**, we have to _include the Audience_. This is done in the Client project by adding the Audience to the appsettings.json and configure authentication to use it as additional provider parameter inprogram.cs:
```cs
options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
```

See
- https://support.auth0.com/center/s/article/opaque-versus-jwt-access-token
- https://auth0.com/blog/securing-blazor-webassembly-apps/