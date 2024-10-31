using Orleans.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Orleans stuff
builder.AddKeyedAzureTableClient("clustering");
builder.AddKeyedAzureBlobClient("grain-state");

builder.UseOrleans(o => o.UseDashboard(x => x.HostSelf = true));

var app = builder.Build();

// app.MapGet("/", () => "OK");
app.Map("/dashboard", x => x.UseOrleansDashboard());

await app.RunAsync();