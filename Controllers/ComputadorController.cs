using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Models;
using Projeto_Dotnet8.Repository;

namespace Projeto_Dotnet8.Controllers;

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

    public IActionResult Editar()
    {
        return View();
    }

    public IActionResult Criar()
    {
        return View();
    }

    /* Preparação para o recebimento dos Dados */
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
    /* Criação de PCs e Salas e limitando a quantidade de PCs por Sala */
    public IActionResult CriarPC(CriarPC_Sala model)
    {
        if (!string.IsNullOrWhiteSpace(model.NovaSalaNum))
        {
            var novaSala = new SalaModels { Sala_Num = model.NovaSalaNum };
            salaRepository.adicionar(novaSala);
            model.SalaSelecionadaId = novaSala.ID;
        }

        int salaId = (model.SalaSelecionadaId != 0) ? model.SalaSelecionadaId : 0;
        var computadoresNaSala = computadorRepository.ListarPorSala(salaId)?.Count() ?? 0;
        if (computadoresNaSala >= 5)
        {
            ModelState.AddModelError("", "Esta sala já possui o limite de 5 computadores.");
            model.Salas = salaRepository.ListarSalas();
            return View(model);
        }

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



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
