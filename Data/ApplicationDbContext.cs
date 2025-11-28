using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projeto_Dotnet8.Models;

namespace Projeto_Dotnet8.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Suas tabelas existentes
        public DbSet<SalaModels> Salas { get; set; }
        public DbSet<ComputadorModels> Computadores { get; set; }
        public DbSet<MensagemModels> Mensagens { get; set; }
    }
}