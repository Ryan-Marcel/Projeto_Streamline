using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Data;
using Projeto_Dotnet8.Models;
using Projeto_Dotnet8.Models.ViewModel;
using Projeto_Dotnet8.Repository;
namespace Reserva.Controllers;

public class Principal : Controller
{
    private readonly ILogger<Principal> _logger;
    private readonly IcomputadorRepository computadorRepository;
    private readonly ISalaRepository salaRepository; 
    private readonly BancoContext _context; 

    public Principal(ILogger<Principal> logger, IcomputadorRepository computadorRepository, ISalaRepository salaRepository, BancoContext context)
    {
        _logger = logger;
        this.computadorRepository = computadorRepository;
        this.salaRepository = salaRepository;
        _context = context;  
    }

    public IActionResult Index()
    {
        return View();
    }

     public IActionResult IndexADM()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
    
         public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult ListarADM()
    {
        var computadores = computadorRepository.ListarComputadores();
        return View(computadores);
    }

    /* Criação das mensagens para cada computador de sua respectiva Sala */
    public IActionResult Criar()
    {
        var salas = salaRepository.ListarSalas();
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
            model.Salas = salaRepository.ListarSalas();
            model.Computadores = model.SalaId != 0
                ? computadorRepository.ListarPorSala(model.SalaId)
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

        model.Salas = salaRepository.ListarSalas();
        model.Computadores = model.SalaId != 0
            ? computadorRepository.ListarPorSala(model.SalaId)
            : new List<ComputadorModels>();
        return View(model);
    }
    /* Listagem dos computadores */
    public IActionResult Editar()
    {
        var computadores = computadorRepository.ListarComputadores(); 
        return View(computadores);
    }
    
    public IActionResult Deletar(int id)
    {
        var computador = computadorRepository.BuscarPorId(id);
        if (computador == null)
            return NotFound();

        return View(computador);
    }

    [HttpPost, ActionName("Deletar")]
    public IActionResult DeletarConfirmado(int id)
    {
        computadorRepository.Deletar(id);
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
        var salas = salaRepository.ListarSalas();
        var computadores = SalaId != 0 ? computadorRepository.ListarPorSala(SalaId) : new List<ComputadorModels>();
        var viewModel = new CriarMensagem
        {
            SalaId = SalaId,
            Salas = salas,
            Computadores = computadores
        };
        return View(viewModel);
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
            return RedirectToAction("ListarADM");
        }
        // Recarregar listas caso falte algum campo
        model.Salas = salaRepository.ListarSalas();
        model.Computadores = model.SalaId != 0 ? computadorRepository.ListarPorSala(model.SalaId) : new List<ComputadorModels>();
        return View(model);
    }
}
