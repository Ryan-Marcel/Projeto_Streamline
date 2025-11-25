using System.Collections.Generic;
using Projeto_Dotnet8.Models.Entities;

namespace Projeto_Dotnet8.Models.ViewModels
{
    public class CriarMensagem
    {

        /* Criação de ViewModel para melhorar a passagem das Informações durante o processo de pesquisa */

        /* Método para a criação de Salas */
        public int SalaId { get; set; }

        /* Método para a criação de Computadores */
        public List<SalaModels>? Salas { get; set; }

        /* Método para a criação de Mensagens */
        public int ComputadorId { get; set; }

        /* Lista de Computadores para auxiliar na criação de Mensagens */
        public List<ComputadorModels>? Computadores { get; set; }

        /* Id da Sala selecionada para a criação do Computador */
        public string? Mensagem { get; set; }
    }
}
