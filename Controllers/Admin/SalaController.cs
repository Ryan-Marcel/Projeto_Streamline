using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Data.Contexts;
using Projeto_Dotnet8.Models.Entities;
using Projeto_Dotnet8.Models.ViewModels;

namespace Projeto_Dotnet8.Controllers.Admin;

public class Sala : Controller
{
    private readonly BancoContext _context;

    public Sala(BancoContext context)
    {
        _context = context;
    }


    public IActionResult Index()
    {
        return View();
    }

    /* Levando os dados para o Listar da controller Principal */
    public IActionResult Listar()
    {
        List<ComputadorModels> computadores = _context.Computadores.ToList();
        return View("~/Views/Principal/Listar.cshtml", computadores);
    }

    public IActionResult Criar()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Criar(SalaModels sala)
    {
        _context.Salas.Add(sala);
        _context.SaveChanges();
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
