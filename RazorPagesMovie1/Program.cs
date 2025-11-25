using Microsoft.EntityFrameworkCore;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages
builder.Services.AddRazorPages();

// Add EF Core + SQL Server
builder.Services.AddDbContext<RazorPagesMovie1Context>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("RazorPagesMovie1Context")));

var app = builder.Build();

// ------------------------------------------
// SEED DATABASE
// ------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error seeding the database.");
    }
}

// ------------------------------------------
// MIDDLEWARE PIPELINE
// ------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Correct: Razor Pages mapping comes last
app.MapRazorPages();

app.Run();
