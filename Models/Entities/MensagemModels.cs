namespace Projeto_Dotnet8.Models.Entities
{
    public class MensagemModels
    {
        public int ID { get; set; }
        public int ComputadorID { get; set; }
        /* Conexão com Computador que vem de ComputadorModels, afim de trazer o contexto de ComputadorModels para MensagemModels e vice versa */
        public ComputadorModels Computador { get; set; }
        public string Texto { get; set; }
    }
}
