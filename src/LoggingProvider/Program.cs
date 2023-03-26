
// Create the logger and setup your sinks, filters and properties
using Serilog;


var builder = WebApplication.CreateBuilder();


Log.Logger = new LoggerConfiguration()
    // Serilog.AspNetCore
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()


    // Add console (Sink) as logging target
    .WriteTo.Console()
    .WriteTo.File("serilog-file.txt")
    // Set default minimum log level
    .MinimumLevel.Debug()

    // Create the actual logger
    .CreateLogger();


// If needed, clear default providers
//builder.Logging.ClearProviders();

// After create the builder - UseSerilog
builder.Host.UseSerilog();

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
