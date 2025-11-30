
using Microsoft.AspNetCore.Identity;
using Projeto_Dotnet8.Models;

namespace Projeto_Dotnet8.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Criar roles se não existirem
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Criar usuário Admin padrão
            var adminRU = "0000000001"; // RU do admin padrão
            var adminUser = await userManager.FindByNameAsync(adminRU);

            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminRU,
                    RU = adminRU,
                    NomeCompleto = "Administrador do Sistema",
                    TipoUsuario = "Admin",
                    DataCadastro = DateTime.Now
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }

            // Criar usuário teste normal
            var userRU = "0000000002"; // RU do usuário normal
            var normalUser = await userManager.FindByNameAsync(userRU);

            if (normalUser == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = userRU,
                    RU = userRU,
                    NomeCompleto = "Usuário Teste",
                    TipoUsuario = "User",
                    DataCadastro = DateTime.Now
                };

                var result = await userManager.CreateAsync(newUser, "User@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "User");
                }
            }
        }
    }
}
