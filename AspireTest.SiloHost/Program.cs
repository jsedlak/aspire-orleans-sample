using Orleans.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Orleans stuff
// builder.AddKeyedAzureTableClient("clustering");
// builder.AddKeyedAzureTableClient("grain-state");
// builder.AddKeyedRedisClient("redis-clustering");

builder.UseOrleans();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.RunAsync();