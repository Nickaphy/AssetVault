var builder = DistributedApplication.CreateBuilder(args);

// Reads the password from the AppHost configuration
var sqlPassword = builder.AddParameter("sql-password", secret: true);
// Declared the SQL serer container
var sqlServer = builder.AddSqlServer("sqlserver",
        password: sqlPassword)
    .WithImageTag("2022-latest") // Keep the same SQL server version
    .WithDataVolume("assetvault_sqlserver-data"); // Mounts the existing database storage `docker volume ls` 

var database = sqlServer.AddDatabase("AssetVault");

// Run the UI
builder.AddProject<Projects.UI>("assetvault-ui")
    .WithReference(database)
    .WaitFor(database); // Delay the UI till the database is ready


builder.Build().Run();