using Minibox.Shared.Library.Setting;
using Minibox.Core.Data.Extension;
using Minibox.Core.Service.Extension;
using Minibox.Shared.Module.Mapping.Extension;
using Minibox.App.Api.Middlewares;
using Minibox.Core.Data.Database.Main;
using Minibox.Shared.Module.Logging.Extension;
using Serilog;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env == "Development")
	builder.Configuration.AddJsonFile($"appsettings.{env}_{Environment.MachineName}.json", true, true);
else
	builder.Configuration.AddJsonFile($"appsettings.{env}.json", true, true);

var connectionString = builder.Configuration.GetConnectionString(nameof(MainDbContext)) ?? string.Empty;

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<MiniboxSettings>(builder.Configuration.GetSection(nameof(MiniboxSettings)));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMainDbContext(builder.Configuration)
	            .AddDataAccessLayer()
	            .AddBussinessLogicLayer()
	            .AddMappingModule()
	            .AddMinIOStorage();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

// Add Serial Logging
builder.ConfigureSerilogLogging(connectionString);

var app = builder.Build();

//Test log
Log.Information("Application started successfully!");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();