using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Projeto_Dotnet8.Data;
using Microsoft.Extensions.DependencyInjection;
using Projeto_Dotnet8.Repository;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();

string MySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseMySql(MySqlConnection, ServerVersion.AutoDetect(MySqlConnection));
});

builder.Services.AddScoped<IcomputadorRepository, ComputadorRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();

// Register Identity with roles using ApplicationDbContext as the store
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Optional: configure password, lockout, etc.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Principal}/{action=Login}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.Initialize(services);
}
app.Run();
