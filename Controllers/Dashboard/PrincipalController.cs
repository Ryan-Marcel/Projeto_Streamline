using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Data.Contexts;
using Projeto_Dotnet8.Models.Entities;
using Projeto_Dotnet8.Models.ViewModels;

namespace Projeto_Dotnet8.Controllers.Dashboard;

[Route("[controller]")]
[ApiController]
public class Principal : Controller
{
    private readonly ILogger<Principal> _logger;
    private readonly BancoContext _context;

    public Principal(ILogger<Principal> logger, BancoContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        // Página do usuário
        return View("~/Views/User/Index.cshtml");
    }

     public IActionResult IndexADM()
    {
        // Página do administrador
        return View("~/Views/Admin/Auth/IndexADM.cshtml");
    }

    public IActionResult Login()
    {
        return View("~/Views/Auth/Login.cshtml");
    }
    
         public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Listar()
    {
        var computadores = _context.Computadores.ToList();
        return View(computadores);
    }

    /* Criação das mensagens para cada computador de sua respectiva Sala */
    public IActionResult Criar()
    {
        var salas = _context.Salas.ToList();
        var viewModel = new CriarMensagem
        {
            Salas = salas,
            Computadores = new List<ComputadorModels>() // Inicialmente vazio
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Criar(CriarMensagem model, string carregar)
    {
        /* Métodos para a escolha da Sala, escolha do Computador após apertar o botão de carregar, que traz os dados dos computadores das sala, 
        e preenche a mensagem do computador selecionado.        
        */
        if (!string.IsNullOrEmpty(carregar))
        {
            model.Salas = _context.Salas.ToList();
            model.Computadores = model.SalaId != 0
                ? _context.Computadores.Where(c => c.SalaModelsID == model.SalaId).ToList()
                : new List<ComputadorModels>();
            return View(model);
        }

        if (!string.IsNullOrWhiteSpace(model.Mensagem) && model.ComputadorId != 0)
        {
            var mensagem = new MensagemModels
            {
                Texto = model.Mensagem,
                ComputadorID = model.ComputadorId
            };
            _context.Mensagens.Add(mensagem);
            _context.SaveChanges();
            return RedirectToAction("Listar");
        }

        model.Salas = _context.Salas.ToList();
        model.Computadores = model.SalaId != 0
            ? _context.Computadores.Where(c => c.SalaModelsID == model.SalaId).ToList()
            : new List<ComputadorModels>();
        return View(model);
    }
    /* Listagem dos computadores */
    public IActionResult Editar()
    {
        var computadores = _context.Computadores.ToList();
        return View(computadores);
    }
    
    public IActionResult Deletar(int id)
    {
        var computador = _context.Computadores.FirstOrDefault(c => c.ID == id);
        if (computador == null)
            return NotFound();

        return View(computador);
    }

    [HttpPost, ActionName("Deletar")]
    public IActionResult DeletarConfirmado(int id)
    {
        var computador = _context.Computadores.FirstOrDefault(c => c.ID == id);
        if (computador != null)
        {
            _context.Computadores.Remove(computador);
            _context.SaveChanges();
        }
        return RedirectToAction("Editar");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // GET: Principal/Solicitacao
    [HttpGet]
    public IActionResult Solicitacao(int SalaId)
    {
        var salas = _context.Salas.ToList();
        var computadores = SalaId != 0 ? _context.Computadores.Where(c => c.SalaModelsID == SalaId).ToList() : new List<ComputadorModels>();
        var viewModel = new CriarMensagem
        {
            SalaId = SalaId,
            Salas = salas,
            Computadores = computadores
        };
        // View está localizada em Views/User/Solicitacao.cshtml
        return View("~/Views/User/Solicitacao.cshtml", viewModel);
    }

    // POST: Principal/Solicitacao
    [HttpPost]
    public IActionResult Solicitacao(CriarMensagem model)
    {
        if (!string.IsNullOrWhiteSpace(model.Mensagem) && model.ComputadorId != 0)
        {
            var mensagem = new MensagemModels
            {
                Texto = model.Mensagem,
                ComputadorID = model.ComputadorId
            };
            _context.Mensagens.Add(mensagem);
            _context.SaveChanges();
            return RedirectToAction("Listar");
        }
        // Recarregar listas caso falte algum campo
        model.Salas = _context.Salas.ToList();
        model.Computadores = model.SalaId != 0 ? _context.Computadores.Where(c => c.SalaModelsID == model.SalaId).ToList() : new List<ComputadorModels>();
        // Garantir renderização da view do usuário
        return View("~/Views/User/Solicitacao.cshtml", model);
    }
}
