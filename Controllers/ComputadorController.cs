using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Data.Contexts;
using Projeto_Dotnet8.Models.Entities;
using Projeto_Dotnet8.Models.ViewModels;

namespace Projeto_Dotnet8.Controllers;

public class Computador : Controller
{
    private readonly BancoContext _context;

    public Computador(BancoContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Index", "Principal");
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
            Salas = _context.Salas.ToList()
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
            _context.Salas.Add(novaSala);
            _context.SaveChanges();
            model.SalaSelecionadaId = novaSala.ID;
        }

        int salaId = (model.SalaSelecionadaId != 0) ? model.SalaSelecionadaId : 0;
        var computadoresNaSala = _context.Computadores.Where(c => c.SalaModelsID == salaId).Count();
        if (computadoresNaSala >= 5)
        {
            ModelState.AddModelError("", "Esta sala já possui o limite de 5 computadores.");
            model.Salas = _context.Salas.ToList();
            return View(model);
        }

        if (salaId != 0)
        {
            var computador = model.Computador;
            computador.SalaModelsID = salaId;
            computador.DataPostagem = DateTime.Now;
            _context.Computadores.Add(computador);
            _context.SaveChanges();
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
