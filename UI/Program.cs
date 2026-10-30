using UI.Components;
using Application.ApplicationService;
using Infrastructure;
using Application.Ports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Dependency Injection, this part handles the creating and supplying.
builder.Services.AddScoped<IAssetImporter, SqlAssetImporter>();
builder.Services.AddScoped <AssetImportService>();

builder.Services.AddScoped<IAssetExporter, SqlAssetExporter>();
builder.Services.AddScoped<AssetExportService>();

builder.Services.AddScoped<IAssetDelete, SqlAssetDeleter>();
builder.Services.AddScoped<AssetDeleteService>();

// register a factory that creates AssetDbContext instances
// use configure those contexts to use Sql server
// read the connectionstring saved in user secrets 
builder.Services.AddDbContextFactory<AssetDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AssetVault")
        ?? throw new InvalidOperationException(
            "The AssetVault connection string is missing.")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();