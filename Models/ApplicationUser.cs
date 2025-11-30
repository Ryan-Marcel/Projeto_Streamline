using Microsoft.AspNetCore.Identity;

namespace Projeto_Dotnet8.Models
{
    public class ApplicationUser : IdentityUser
    {
        // RU é o código de 10 dígitos (será usado como UserName)
        public string? RU { get; set; }
        
        // Nome completo do usuário
        public string? NomeCompleto { get; set; }
        
        // Tipo de usuário: "Admin" ou "User"
        public string TipoUsuario { get; set; } = "User";
        
        // Data de cadastro
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
