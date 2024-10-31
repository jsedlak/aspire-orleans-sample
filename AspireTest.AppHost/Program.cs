var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage").RunAsEmulator();

var grainStorage = storage.AddBlobs("grain-state");
var cluster = storage.AddTables("clustering");

var orleans = builder.AddOrleans("default")
    .WithClustering(cluster)
    .WithGrainStorage("Default", grainStorage);

var siloProject = builder.AddProject<Projects.AspireTest_SiloHost>("silo")
    .WithReference(orleans)
    .WaitFor(grainStorage)
    .WaitFor(cluster)
    .WithReplicas(3);

var apiService = builder.AddProject<Projects.AspireTest_ApiService>("apiservice")
    .WithReference(orleans.AsClient())
    .WaitFor(siloProject)
    .WithReplicas(3);

builder.AddProject<Projects.AspireTest_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);



builder.Build().Run();
