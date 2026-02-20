using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazingTrails.Client;
using BlazingTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Security.Claims;
using BlazingTrails.Client.Features.Auth;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddHttpClient(Constants.SecureAPIClient, client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
    .AddAsKeyed();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddTrailHandler>());

builder.Services
    .AddOidcAuthentication(options =>
        {
            // use the settings provided by the configuration, here the appsettings.json file
            builder.Configuration.Bind("Auth0", options.ProviderOptions);

            // use the Authorization Code flow
            options.ProviderOptions.ResponseType = "code";

            options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
            options.UserOptions.NameClaim = ClaimTypes.Email;
        })
    .AddAccountClaimsPrincipalFactory<CustomUserFactory<RemoteUserAccount>>();

await builder.Build().RunAsync();