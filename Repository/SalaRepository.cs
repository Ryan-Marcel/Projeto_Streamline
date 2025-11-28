using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto_Dotnet8.Data;
using Projeto_Dotnet8.Models;

namespace Projeto_Dotnet8.Repository
{
    public class SalaRepository : ISalaRepository
    {

        private readonly ApplicationDbContext salaContext;
        public SalaRepository(ApplicationDbContext sala_Context)
        {
            salaContext = sala_Context;
        }

        public SalaModels adicionar(SalaModels Sala)
        {
            salaContext.Salas.Add(Sala);
            salaContext.SaveChanges();
            return Sala;
        }
        /* Método que Procura e Lista os Computadores existentes */
        public List<ComputadorModels> ListarComputadores()
        {
            return salaContext.Computadores.ToList();
        }

        /* Método que Procura e Lista as Salas existentes */
        public List<SalaModels> ListarSalas()
        {
            return salaContext.Salas.ToList();
        }
    }
}