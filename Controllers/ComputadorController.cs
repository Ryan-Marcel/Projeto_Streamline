using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Models;
using Projeto_Dotnet8.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Projeto_Dotnet8.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Computador : Controller
    {
        private readonly IcomputadorRepository computadorRepository;
        private readonly ISalaRepository salaRepository; 

        public Computador(IcomputadorRepository computador_Repository, ISalaRepository salaRepository)
        {
            computadorRepository = computador_Repository;
            this.salaRepository = salaRepository;
        }

        public IActionResult Index()
        {
            return RedirectToAction("IndexADM", "Principal");
        }

        // GET: Página principal com abas
        public IActionResult CriarPC()
        {
            var viewModel = new CriarPC_Sala
            {
                Computador = new ComputadorModels(),
                Salas = salaRepository.ListarSalas()
            };
            return View(viewModel);
        }

        // POST: Criar Computador
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriarComputador(CriarPC_Sala model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Computador.Nome))
                {
                    TempData["Erro"] = "O nome do computador é obrigatório.";
                    model.Salas = salaRepository.ListarSalas();
                    return View("CriarPC", model);
                }

                if (model.SalaSelecionadaId == 0)
                {
                    TempData["Erro"] = "Selecione uma sala para o computador.";
                    model.Salas = salaRepository.ListarSalas();
                    return View("CriarPC", model);
                }

                // Limite removido - sem restrição de computadores por sala

                var computador = model.Computador;
                computador.SalaModelsID = model.SalaSelecionadaId;
                computador.DataPostagem = DateTime.Now;
                computadorRepository.adicionar(computador);

                TempData["Sucesso"] = "Computador criado com sucesso!";
                return RedirectToAction("CriarPC");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Erro ao criar computador: {ex.Message}";
                model.Salas = salaRepository.ListarSalas();
                return View("CriarPC", model);
            }
        }

        // POST: Criar Sala
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CriarSala(CriarPC_Sala model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.NovaSalaNum))
                {
                    TempData["Erro"] = "O número/nome da sala é obrigatório.";
                    model.Salas = salaRepository.ListarSalas();
                    return View("CriarPC", model);
                }

                // Verifica se já existe uma sala com esse número
                var salaExistente = salaRepository.ListarSalas()
                    .Any(s => s.Sala_Num.Equals(model.NovaSalaNum, StringComparison.OrdinalIgnoreCase));

                if (salaExistente)
                {
                    TempData["Erro"] = "Já existe uma sala com este número/nome.";
                    model.Salas = salaRepository.ListarSalas();
                    return View("CriarPC", model);
                }

                var novaSala = new SalaModels { Sala_Num = model.NovaSalaNum };
                salaRepository.adicionar(novaSala);

                TempData["Sucesso"] = "Sala criada com sucesso!";
                return RedirectToAction("CriarPC");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Erro ao criar sala: {ex.Message}";
                model.Salas = salaRepository.ListarSalas();
                return View("CriarPC", model);
            }
        }

        public IActionResult Excluir()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}