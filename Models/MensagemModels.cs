namespace Projeto_Dotnet8.Models
{
    public class MensagemModels
    {
        public int ID { get; set; }
        public int ComputadorID { get; set; }
        
        /* Conexão com Computador que vem de ComputadorModels */
        public ComputadorModels? Computador { get; set; }
        
        public string? Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        
        // Novo campo: ID do usuário que criou a mensagem
        public string? UserId { get; set; }
        
        // Relacionamento com o usuário
        public ApplicationUser? Usuario { get; set; }
        
        // Campo de Status
        // 0 = Em Aberto, 1 = Em Andamento, 2 = Resolvido
        public int Status { get; set; } = 0;

        public MensagemModels()
        {
            DataCriacao = DateTime.Now;
        }
    }
}