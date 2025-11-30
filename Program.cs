using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Projeto_Dotnet8.Data;
using Microsoft.Extensions.DependencyInjection;
using Projeto_Dotnet8.Repository;
using Projeto_Dotnet8.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Configuração do MySQL
string MySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BancoContext>(opt =>
{
    opt.UseMySql(MySqlConnection, ServerVersion.AutoDetect(MySqlConnection));
});

// Configuração do Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configurações de senha
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    
    // Configurações de usuário
    options.User.RequireUniqueEmail = false;
    
    // Configurações de bloqueio de conta
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<BancoContext>()
.AddDefaultTokenProviders();

// Configuração de cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Principal/Login";
    options.LogoutPath = "/Principal/Logout";
    options.AccessDeniedPath = "/Principal/AcessoNegado";
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
    options.SlidingExpiration = true;
});

// Repositórios
builder.Services.AddScoped<IcomputadorRepository, ComputadorRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();

var app = builder.Build();

// Seed de dados iniciais (roles e admin)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao executar seed de dados");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Importante: Authentication vem antes de Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Principal}/{action=Login}/{id?}");

app.Run();
