using EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// retrieve database connection from config
string completeConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// add version options to our program
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// add swagger w. options
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Add entity framework core services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    optionsBuilder => optionsBuilder.UseNpgsql(
        completeConnectionString,
        options => options.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromMilliseconds(100),
            errorCodesToAdd: null))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
    ServiceLifetime.Transient);

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();
