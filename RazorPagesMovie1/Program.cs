using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie1.Data;
using RazorPagesMovie1.Models;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------
// ADD DATABASE CONTEXTS
// ------------------------------
builder.Services.AddDbContext<RazorPagesMovie1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPagesMovie1Context")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContextConnection")));

// ------------------------------
// ADD IDENTITY
// ------------------------------
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ------------------------------
// ADD AUTHORIZATION POLICY
// ------------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// ------------------------------
// ADD RAZOR PAGES, FORM OPTIONS & SESSION
// ------------------------------
builder.Services.AddRazorPages();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 268435456; // 256 MB
});
builder.Services.AddSession(); // ✅ Moved BEFORE builder.Build()

var app = builder.Build();

// ------------------------------
// SEED DATABASE (MOVIES ETC.)
// ------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services); // Your existing seed method
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error seeding the database.");
    }
}

// ------------------------------
// CREATE ADMIN ROLE & USER
// ------------------------------
async Task CreateAdminRole(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Create Admin role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    // Create Admin user if it doesn't exist
    string adminEmail = "admin@movies.com";
    string adminPassword = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, adminPassword);
    }

    // Add user to Admin role if not already
    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        await userManager.AddToRoleAsync(adminUser, "Admin");
}

// Run admin creation
using (var scope = app.Services.CreateScope())
{
    await CreateAdminRole(scope.ServiceProvider);
}

// ------------------------------
// MIDDLEWARE
// ------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // ✅ Session middleware
app.UseAuthentication();
app.UseAuthorization();

// ------------------------------
// MAP RAZOR PAGES
// ------------------------------
app.MapRazorPages();

app.Run();
