using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Data;
using Projeto_Dotnet8.Models;
using Projeto_Dotnet8.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
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

    public IActionResult EditarNotif()
    {
        return View();
    }

   public IActionResult Salas()
{
    var salas = salaRepository.ListarSalas();
    
    // Para cada sala, buscar seus computadores
    foreach (var sala in salas)
    {
        sala.Computadores = computadorRepository.ListarPorSala(sala.ID).ToList();
    }
    
    return View(salas);
}

public IActionResult DetalhesComputador(int id)
{
    var computador = computadorRepository.BuscarPorId(id);
    
    if (computador == null)
        return NotFound();
    
    // Carregar as mensagens do computador
    computador.Mensagens = _context.Mensagens
        .Where(m => m.ComputadorID == id)
        .OrderByDescending(m => m.ID)
        .ToList();
    
    return View(computador);
}


    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // TODO: Implementar validação de credenciais com o banco de dados
        // Por enquanto, redireciona para Index se os campos forem preenchidos
        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
        {
            return RedirectToAction("Index");
        }
        
        ModelState.AddModelError("", "Registro do Aluno ou Senha inválidos");
        return View();
    }
    
         public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult ListarADM(int? statusFiltro)
{
    // Buscar todas as mensagens com seus computadores
    var mensagens = _context.Mensagens
    .Include(m => m.Computador)  
    .OrderByDescending(m => m.DataCriacao)
    .AsQueryable();

    // Aplicar filtro de status se especificado
    if (statusFiltro.HasValue)
    {
        mensagens = mensagens.Where(m => m.Status == statusFiltro.Value);
    }

    ViewBag.StatusFiltro = statusFiltro;
    return View(mensagens.ToList());
}

public class AtualizarStatusRequest
{
    public int id { get; set; }
    public int novoStatus { get; set; }
}

[HttpPost]
public IActionResult AtualizarStatus([FromBody] AtualizarStatusRequest request)
{
    try
    {
        var mensagem = _context.Mensagens.FirstOrDefault(m => m.ID == request.id);
        if (mensagem != null)
        {
            mensagem.Status = request.novoStatus;
            _context.SaveChanges();
            return Json(new { success = true });
        }
        return Json(new { success = false, error = "Mensagem não encontrada" });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, error = ex.Message });
    }
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