using PowerpointAi.Models;
using PowerpointAi.Services;

var builder = WebApplication.CreateBuilder(args);

// Load Foundry config
builder.Services.Configure<FoundryOptions>(
    builder.Configuration.GetSection("Foundry"));

// DI
builder.Services.AddSingleton<FoundryClientWrapper>();
builder.Services.AddScoped<AgentService>();
builder.Services.AddScoped<Orchestrator>();
builder.Services.AddScoped<PowerPointService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
