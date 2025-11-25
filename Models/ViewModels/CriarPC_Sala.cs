using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto_Dotnet8.Models.Entities;

namespace Projeto_Dotnet8.Models.ViewModels
{
    public class CriarPC_Sala
    {
        
        /* Criação de ViewModel para melhorar a passagem das Informações durante o processo de pesquisa */

        /* Método para a criação de Salas */
        public string? NovaSalaNum { get; set; }

        /* Método para a criação de Computadores */
        public ComputadorModels? Computador { get; set; }

        /* Lista de Salas para auxiliar na criação de Computadores */
        public List<SalaModels>? Salas { get; set; }

        /* Id da Sala selecionada para a criação do Computador */
        public int SalaSelecionadaId { get; set; }
    }
}
