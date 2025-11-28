using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Models;
using Projeto_Dotnet8.Repository;

namespace Projeto_Dotnet8.Controllers
{
    [Authorize(Roles = "Admin")] // Toda a área de computadores exige Administrador
    public class ComputadorController : Controller
    {
        private readonly IcomputadorRepository computadorRepository;
        private readonly ISalaRepository salaRepository;

        public ComputadorController(
            IcomputadorRepository computadorRepository,
            ISalaRepository salaRepository)
        {
            this.computadorRepository = computadorRepository;
            this.salaRepository = salaRepository;
        }

        // Redireciona para a página principal
        public IActionResult Index()
        {
            return RedirectToAction("IndexADM", "Principal");
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }

        // ----------------------
        //  CRIAÇÃO DE PC + SALA
        // ----------------------

        public IActionResult CriarPC()
        {
            var viewModel = new CriarPC_Sala
            {
                Computador = new ComputadorModels(),
                Salas = salaRepository.ListarSalas()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CriarPC(CriarPC_Sala model)
        {
            // Caso o usuário deseje criar uma NOVA sala no mesmo formulário
            if (!string.IsNullOrWhiteSpace(model.NovaSalaNum))
            {
                var novaSala = new SalaModels { Sala_Num = model.NovaSalaNum };
                salaRepository.adicionar(novaSala);

                model.SalaSelecionadaId = novaSala.ID; // Define a sala criada como selecionada
            }

            int salaId = model.SalaSelecionadaId != 0 ? model.SalaSelecionadaId : 0;

            // Limite de computadores por sala (máx. 5)
            var computadoresNaSala = computadorRepository.ListarPorSala(salaId)?.Count() ?? 0;

            if (computadoresNaSala >= 5)
            {
                ModelState.AddModelError("", "Esta sala já possui o limite de 5 computadores.");
                model.Salas = salaRepository.ListarSalas();
                return View(model);
            }

            // Criar o computador caso tudo esteja correto
            if (salaId != 0)
            {
                var computador = model.Computador;
                computador.SalaModelsID = salaId;
                computador.DataPostagem = DateTime.Now;

                computadorRepository.adicionar(computador);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Excluir()
        {
            return View();
        }

        // ----------------------
        // TRATAMENTO DE ERROS
        // ----------------------
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
