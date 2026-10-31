using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using Application.ApplicationService;
using Core.Entities;
using Core.Enums;

namespace UI.Endpoints;


// An outside application sends JSON to POST /api/assets/ the record holds these values
public record CreateAssetRequest(
    string Name, 
    AssetType Type,
    DateOnly PurchaseDate);

public static class AssetEndpoints
{
    public static IEndpointRouteBuilder MapAssetEndpoints( // ASP.NET core's interface for registering routes.
        this IEndpointRouteBuilder app)
    {
        var assetRoutes = app.MapGroup("/api/assets"); // Creates a route group with a shared URL

        // Get assets
        assetRoutes.MapGet("/", (AssetImportService importService) => // Registers a handler for /api/assets/
        {
            var result = importService.ImportAssets(); // Import the assets like normal

            // Returns the JSON aray of asset objects and HTTP 200 (OK)
            return Results.Ok(result.Select(asset => new
            {
                id = asset.Id,
                name = asset.Name,
                type = asset.Type.ToString(),
                purchaseDate = DateOnly.FromDateTime(asset.PurchaseDate),
                isAssigned = asset.IsAssigned
            }));
        });
        
        // Post asset
        assetRoutes.MapPost("/", (CreateAssetRequest request, AssetExportService exportService) =>
        {
            // Reads the request record with the JSON and creates the Asset object
            var asset = Asset.CreateAsset(
                request.Name,
                request.Type,
                request.PurchaseDate.ToDateTime(TimeOnly.MinValue));
            exportService.ExportAsset(new List<Asset> { asset });
        });
        
        // Delete asset by ID
        assetRoutes.MapDelete("/{assetId:guid}", (AssetDeleteService deleteService, Guid assetId) =>
        {
            deleteService.DeleteAsset(assetId);
            return Results.NoContent();
        });
        
        
        return app;
    }
}