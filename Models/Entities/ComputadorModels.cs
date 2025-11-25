using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Dotnet8.Models.Entities
{
    public class ComputadorModels
    {
    public int ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int SalaModelsID { get; set; }
    public DateTime DataPostagem { get; set; } // Horário de criação/postagem
    /* Trazendo Mensagens de MensagemModels, para a conexão e identificação entre as 2 Models */
    public List<MensagemModels> Mensagens { get; set; } = new List<MensagemModels>();
    }
}
