using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BakeryManagementPOSWebApp.Components;
using BakeryManagementPOSWebApp.Components.Account;
using BakeryManagementPOSWebApp.Data;
using BakeryManagementPOSWebApp.Data.Enities;
using BakeryManagementPOSWebApp.Services.Products;
using BakeryManagementPOSWebApp.Services.Customers;
using BakeryManagementPOSWebApp.Services.Employees;
using BakeryManagementPOSWebApp.Services.Orders;

static void consoleMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(message);
    Console.ForegroundColor = ConsoleColor.White;
}

consoleMessage("[s] - Server Starting!");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();

consoleMessage("[s] - Added Components");

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

consoleMessage("[s] - Added Scoped Services");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies(options => options.ApplicationCookie!.Configure(options => {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        }));

consoleMessage("[s] - Added and Configured Authentication");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<Employee>(options => 
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequiredLength = 4;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Lockout.MaxFailedAccessAttempts = 10;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

consoleMessage("[s] - Added and Configured DB Service");

var app = builder.Build();

consoleMessage("[s] - Build Successful!");

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    consoleMessage("[s] - DB Service Start Successful!");

    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        consoleMessage("[s] - DB Migration Start");

        context.Database.Migrate();

        consoleMessage("[s] - DB Migration Finished. DB is up to date!");
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("[s] - DB was unable to Migrate. See detailed error below.");
        Console.WriteLine(ex.Message, ex.ToString());
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseMigrationsEndPoint();
}
else
{
    app.Urls.Add("http://0.0.0.0:5000");
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

consoleMessage("[s] - Mapping Complete");

consoleMessage("[s] - Encounter Tracker is starting and will run on \"http://localhost:5000\"");

app.Run();
