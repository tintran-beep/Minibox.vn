using Minibox.Shared.Library.Setting;
using Minibox.Core.Data.Extension;
using Minibox.Core.Service.Extension;
using Minibox.Shared.Module.Mapping.Extension;


var builder = WebApplication.CreateBuilder(args);
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (env == "Development")
	builder.Configuration.AddJsonFile($"appsettings.{env}_{Environment.MachineName}.json", true, true);
else
	builder.Configuration.AddJsonFile($"appsettings.{env}.json", true, true);

var conStr = builder.Configuration.GetConnectionString("MainDbContext");

builder.Configuration.AddEnvironmentVariables();

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

//Auto Migration
await app.Services.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();