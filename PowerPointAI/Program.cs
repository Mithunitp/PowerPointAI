using Azure;
using Azure.AI.Projects;
using PowerpointAi.Services;

var builder = WebApplication.CreateBuilder(args);

var azureConfig = builder.Configuration.GetSection("AzureAI");

// Register Foundry client
builder.Services.AddSingleton(sp =>
{
    var endpoint = new Uri(azureConfig["Endpoint"]!);
    var apiKey = new AzureKeyCredential(azureConfig["ApiKey"]!);
    return new AIProjectClient(endpoint, apiKey);
});

// Add services
builder.Services.AddScoped<FoundryService>();
builder.Services.AddScoped<OrchestratorService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
