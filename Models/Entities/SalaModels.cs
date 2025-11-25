using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Dotnet8.Models.Entities
{
    public class SalaModels
    {
        public int ID { get; set; }
        public string Sala_Num { get; set; }
        public List<ComputadorModels> Computadores { get; set; }
    }
}
