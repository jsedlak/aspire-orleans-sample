var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage").RunAsEmulator();

var grainStorage = storage.AddBlobs("grain-state");
var cluster = storage.AddTables("clustering");

var orleans = builder.AddOrleans("default")
    .WithClustering(cluster)
    .WithGrainStorage(grainStorage);

var apiService = builder.AddProject<Projects.AspireTest_ApiService>("apiservice")
    .WithReplicas(3);

builder.AddProject<Projects.AspireTest_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.AddProject<Projects.AspireTest_SiloHost>("silo")
    .WithReference(orleans)
    .WaitFor(grainStorage)
    .WaitFor(cluster)
    .WithReplicas(3);

builder.Build().Run();
