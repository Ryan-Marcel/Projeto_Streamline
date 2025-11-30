
using System.ComponentModel.DataAnnotations;

namespace Projeto_Dotnet8.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O RU é obrigatório")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "O RU deve ter exatamente 10 dígitos")]
        public string RU { get; set; } = string.Empty;

     [Required(ErrorMessage = "O nome completo é obrigatório")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione o tipo de usuário")]
        public string TipoUsuario { get; set; } = "User";
    }
}
