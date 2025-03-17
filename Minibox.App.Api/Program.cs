using Minibox.Shared.Library.Setting;
using Minibox.Core.Data.Extension;
using Minibox.Core.Service.Extension;
using Minibox.Shared.Module.Mapping.Extension;
using Minibox.App.Api.Middlewares;
using Minibox.Core.Data.Database.Main;
using Minibox.Shared.Module.Logging.Extension;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env == "Development")
	builder.Configuration.AddJsonFile($"appsettings.{env}_{Environment.MachineName}.json", true, true);
else
	builder.Configuration.AddJsonFile($"appsettings.{env}.json", true, true);

var connectionString = builder.Configuration.GetConnectionString(nameof(MainDbContext)) ?? string.Empty;

builder.Configuration.AddEnvironmentVariables();

builder.ConfigureSerilogLogging(connectionString);

builder.Services.Configure<MiniboxSettings>(builder.Configuration.GetSection(nameof(MiniboxSettings)));

// Add services to the container.
builder.Services.AddControllers();

builder.Services
	.AddMainDbContext(builder.Configuration)
	.AddDataAccessLayer()
	.AddBussinessLogicLayer()
	.AddMappingModule();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

//Auto Migration
//await app.Services.MigrateAsync();

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