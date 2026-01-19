using System.Reflection;
using BlazingTrails.Persistence;
using BlazingTrails.Shared.Features.ManageTrails;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlazingTrailsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext"))
);
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<TrailValidator>();


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
