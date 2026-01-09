var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable debugging of webassembly code
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

// Enable to serve the Blazor application
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapControllers();
// If no controller matches the request, serve the index file from the Blazor client
app.MapFallbackToFile("index.html");

app.Run();
