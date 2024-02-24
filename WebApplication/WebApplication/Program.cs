using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// retrieve from config
string completeConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    optionsBuilder => optionsBuilder.UseNpgsql(
        completeConnectionString,
        options => options.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromMilliseconds(100),
            errorCodesToAdd: null))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
    ServiceLifetime.Transient);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
