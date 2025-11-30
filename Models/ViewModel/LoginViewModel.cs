
using System.ComponentModel.DataAnnotations;

namespace Projeto_Dotnet8.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O RU é obrigatório")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "O RU deve ter exatamente 10 dígitos")]
        public string RU { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
