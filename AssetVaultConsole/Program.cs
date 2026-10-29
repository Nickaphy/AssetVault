using System.Globalization;
using Core.Enums;
using Core.Entities;
using Application.ApplicationService;
using Infrastructure;

public class Program
{
    public static void Main()
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "assets.txt");
        var importService = new AssetImportService(new AssetImporter(filePath));
        var exportService = new AssetExportService(new TempAssetExporter(filePath));
        
        if (true)
        {
            Console.Clear();
            Console.Write("[1] Add Asset\n[2] View Assets\nChoice: ");
            string choice =  Console.ReadLine();
            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Invalid");
                Thread.Sleep(1000);
            }
            else
            {
                switch(choice)
                {
                    case "1":
                        AddAsset(exportService);
                        break;
                    case "2":
                        ViewAssets(importService);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
        }
    }
    
    public static void AddAsset(AssetExportService exportService)
    {
        Console.Clear();
        List<Asset> assetList = new List<Asset>();
        
        while (true)
        {
            Console.Write("Enter Asset Name: ");
            string? assetName = Console.ReadLine()?.Trim();
            Console.WriteLine("[Laptop]\n[Desktop]\n[Server]");
            Console.Write("Enter Type: ");
            string? assetTypeString = Console.ReadLine()?.Trim();
            assetTypeString = char.ToUpper(assetTypeString[0]) + assetTypeString[1..]; //Convert first char to upper, and leave the rest
            Console.Write("Enter Purchase Date (yyyy-MM-dd): ");
            string? purchaseDateString = Console.ReadLine();

            // Attempt to parse our string to DateTime.
            bool validPurchaseDateBool = DateTime.TryParseExact(
                purchaseDateString,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime purchaseDate);
            
            
            if (Enum.TryParse<AssetType>(assetTypeString, out AssetType assetType)
                && Enum.IsDefined(assetType) && validPurchaseDateBool 
                && purchaseDate <= DateTime.Today 
                && !string.IsNullOrEmpty(assetName))
            {
                Console.WriteLine($"Selected: {assetType}");
                Console.WriteLine($"Purchase Date: {purchaseDate}");
                Thread.Sleep(2500);
                Console.Clear();

                // Create the entity
                assetList.Add(Asset.CreateAsset(assetName, assetType, purchaseDate));

                try
                {
                    exportService.ExportAsset(assetList);
                    Console.WriteLine("Assets exported successfully.");
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
                {
                    Console.WriteLine($"Export failed: {ex.Message}");
                    Console.WriteLine("The assets are still available in memory.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Asset");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    public static void ViewAssets(AssetImportService importService)
    {
        bool searchById;
        Guid id = Guid.Empty;
        
        while (true)
        {
            Console.Clear();
            Console.Write("Search by ID (press Enter to view all): ");
            string? searchString = Console.ReadLine()?.Trim();
            
            searchById = !string.IsNullOrEmpty(searchString);
            if (searchById && !Guid.TryParse(searchString, out id))
            {
                Console.WriteLine("Invalid ID format.");
                continue;
            }
            break;
        }

        try
        {
            var assets = importService.ImportAssets();
            if (searchById)
            {
                Asset? matchingAsset = assets.FirstOrDefault(asset => asset.Id == id);
                if (matchingAsset is null)
                {
                    Console.WriteLine("No asset found with that ID.");
                    Thread.Sleep(1000);
                    return;
                }

                DisplayAsset(matchingAsset);
                return;
            }

            bool anyAssets = false;
            foreach (Asset asset in assets)
            {
                anyAssets = true;
                DisplayAsset(asset);
            }

            if (!anyAssets)
                Console.WriteLine("No assets found.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("No asset file found. Register an asset first.");
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or FormatException or ArgumentException)
        {
            Console.WriteLine($"Could not load assets: {ex.Message}");
        }
    }

    private static void DisplayAsset(Asset asset)
    {
        Console.WriteLine($"ID: {asset.Id}");
        Console.WriteLine($"Name: {asset.Name}");
        Console.WriteLine($"Type: {asset.Type}");
        Console.WriteLine($"Purchase date: {asset.PurchaseDate:yyyy-MM-dd}");
        Console.WriteLine($"Assigned: {asset.IsAssigned}");
        Console.WriteLine();
    }
}
