/*
 * File responsible for orchestrating the persistence of the created asset. 
 */

using Core.Entities;
using Application.Ports;

namespace Application.ApplicationService;

public class AssetExportService
{
    private readonly IAssetExporter exporter;   
    // Constructor recieves the exporter
    public AssetExportService(IAssetExporter exporter)
    {
        this.exporter = exporter;
    }
    
    public void ExportAsset(List<Asset> assets) 
    {
        exporter.Export(assets); // Calls the Export Contract
    }
}