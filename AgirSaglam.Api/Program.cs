using AgirSaglam.Repository;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using NLog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//NLog
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddNLog();
});





//database
builder.Services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer("Data Source=DESKTOP-KSSIRCE; Initial Catalog=AgirSaglamDb; Integrated Security=true; TrustServerCertificate=True"));
builder.Services.AddScoped<RepositoryWrapper, RepositoryWrapper>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//iç içe döngü varsa serileþmeyi önlemek için 
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//cash için
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
//Error
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
