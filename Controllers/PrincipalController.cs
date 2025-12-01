using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_Dotnet8.Data;
using Projeto_Dotnet8.Models;
using Projeto_Dotnet8.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Projeto_Dotnet8.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Reserva.Controllers;

public class Principal : Controller
{
    private readonly ILogger<Principal> _logger;
    private readonly IcomputadorRepository computadorRepository;
    private readonly ISalaRepository salaRepository;
    private readonly BancoContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Principal(ILogger<Principal> logger, IcomputadorRepository computadorRepository,
                     ISalaRepository salaRepository, BancoContext context,
                     UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
        this.computadorRepository = computadorRepository;
        this.salaRepository = salaRepository;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Página inicial - requer autenticação
    [Authorize(Roles = "User")]
    public IActionResult Index()
    {
        return View();
    }

    // Página ADM - só para administradores
    [Authorize(Roles = "Admin")]
    public IActionResult IndexADM()
    {
        return View();
    }

    // Login - não requer autenticação
    [AllowAnonymous]
    public IActionResult Login()
    {
        // Se já estiver autenticado, redireciona
        if (User.Identity?.IsAuthenticated == true)
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("IndexADM");
            else
                return RedirectToAction("Index");
        }
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Login usando o RU como UserName
            var result = await _signInManager.PasswordSignInAsync(
                model.RU,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.RU);

                // Redireciona baseado no tipo de usuário
                if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("IndexADM");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Conta bloqueada. Tente novamente mais tarde.");
            }
            else
            {
                ModelState.AddModelError("", "RU ou Senha inválidos");
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    [AllowAnonymous]
    public IActionResult AcessoNegado()
    {
        return View();
    }

    [Authorize]
    public IActionResult EditarNotif()
    {
        return View();
    }

    [Authorize]
    public IActionResult Salas()
    {
        var salas = salaRepository.ListarSalas();

        foreach (var sala in salas)
        {
            sala.Computadores = computadorRepository.ListarPorSala(sala.ID).ToList();
        }

        return View(salas);
    }

    [Authorize]
    public IActionResult DetalhesComputador(int id)
    {
        var computador = computadorRepository.BuscarPorId(id);

        if (computador == null)
            return NotFound();

        computador.Mensagens = _context.Mensagens
            .Where(m => m.ComputadorID == id)
            .OrderByDescending(m => m.ID)
            .ToList();

        return View(computador);
    }

    [Authorize]
    public IActionResult Dashboard()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult ListarADM(int? statusFiltro)
    {
        var mensagens = _context.Mensagens
            .Include(m => m.Computador)
            .OrderByDescending(m => m.DataCriacao)
            .AsQueryable();

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
    [Authorize(Roles = "Admin")]
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

    // ========== MÉTODOS CRIAR ATUALIZADOS ==========
    [Authorize(Roles = "User")]
    public IActionResult Criar(int? SalaId)
    {
        var salas = salaRepository.ListarSalas();
        var viewModel = new CriarMensagem
        {
            Salas = salas,
            SalaId = SalaId ?? 0,
            Computadores = SalaId.HasValue && SalaId.Value != 0 
                ? computadorRepository.ListarPorSala(SalaId.Value) 
                : new List<ComputadorModels>()
        };
        return View(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    [ValidateAntiForgeryToken]
    public IActionResult Criar(CriarMensagem model)
    {
        // Validação da Sala
        if (model.SalaId == 0)
        {
            TempData["Erro"] = "Selecione uma sala.";
            model.Salas = salaRepository.ListarSalas();
            model.Computadores = new List<ComputadorModels>();
            return View(model);
        }

        // Validação do Computador
        if (model.ComputadorId == 0)
        {
            TempData["Erro"] = "Selecione um computador.";
            model.Salas = salaRepository.ListarSalas();
            model.Computadores = computadorRepository.ListarPorSala(model.SalaId);
            return View(model);
        }

        // Validação da Mensagem
        if (string.IsNullOrWhiteSpace(model.Mensagem))
        {
            TempData["Erro"] = "Descreva o problema.";
            model.Salas = salaRepository.ListarSalas();
            model.Computadores = computadorRepository.ListarPorSala(model.SalaId);
            return View(model);
        }

        try
        {
            var userId = _userManager.GetUserId(User);
            
            var mensagem = new MensagemModels
            {
                Texto = model.Mensagem,
                ComputadorID = model.ComputadorId,
                UserId = userId,
                DataCriacao = DateTime.Now,
                Status = 0
            };
            
            _context.Mensagens.Add(mensagem);
            _context.SaveChanges();

            TempData["Sucesso"] = "Solicitação enviada com sucesso!";
            return RedirectToAction("HistoricoSolicitacoes");
        }
        catch (Exception ex)
        {
            TempData["Erro"] = $"Erro ao criar solicitação: {ex.Message}";
            model.Salas = salaRepository.ListarSalas();
            model.Computadores = computadorRepository.ListarPorSala(model.SalaId);
            return View(model);
        }
    }

    // Método usado pelo AJAX para carregar computadores
    [HttpGet]
    [Authorize(Roles = "User")]
    public IActionResult Solicitacao(int SalaId)
    {
        var computadores = SalaId != 0 
            ? computadorRepository.ListarPorSala(SalaId) 
            : new List<ComputadorModels>();

        return PartialView("_ComputadoresGrid", computadores);
    }
    // ===============================================

    [Authorize(Roles = "User")]
    public IActionResult Editar()
    {
        var computadores = computadorRepository.ListarComputadores();
        return View(computadores);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Deletar(int id)
    {
        var computador = computadorRepository.BuscarPorId(id);
        if (computador == null)
            return NotFound();

        return View(computador);
    }

    [HttpPost, ActionName("Deletar")]
    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "User")]
    public IActionResult HistoricoSolicitacoes()
    {
        // Pega o ID do usuário logado
        var userId = _userManager.GetUserId(User);
        
        // Busca APENAS as mensagens do usuário logado
        var mensagens = _context.Mensagens
            .Where(m => m.UserId == userId) // Filtra por usuário
            .Include(m => m.Computador)
            .OrderByDescending(m => m.DataCriacao)
            .ToList();
        
        return View(mensagens);
    }

    // POST: Atualizar Mensagem
    [HttpPost]
    [Authorize(Roles = "User")]
    public IActionResult AtualizarMensagem([FromBody] AtualizarMensagemRequest request)
    {
        try
        {
            var mensagem = _context.Mensagens.FirstOrDefault(m => m.ID == request.id);
            
            if (mensagem == null)
            {
                return Json(new { success = false, error = "Mensagem não encontrada" });
            }
            
            // Validação: Verifica se o usuário é o dono da mensagem
            var userId = _userManager.GetUserId(User);
            if (mensagem.UserId != userId)
            {
                return Json(new { success = false, error = "Você não tem permissão para editar esta mensagem" });
            }
            
            // Atualiza a mensagem
            mensagem.Texto = request.novoTexto;
            _context.SaveChanges();
            
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    // POST: Excluir Mensagem
    [HttpPost]
    [Authorize(Roles = "User")]
    public IActionResult ExcluirMensagem([FromBody] ExcluirMensagemRequest request)
    {
        try
        {
            var mensagem = _context.Mensagens.FirstOrDefault(m => m.ID == request.id);
            
            if (mensagem == null)
            {
                return Json(new { success = false, error = "Mensagem não encontrada" });
            }
            
            // Validação: Verifica se o usuário é o dono da mensagem
            var userId = _userManager.GetUserId(User);
            if (mensagem.UserId != userId)
            {
                return Json(new { success = false, error = "Você não tem permissão para excluir esta mensagem" });
            }
            
            // Exclui a mensagem
            _context.Mensagens.Remove(mensagem);
            _context.SaveChanges();
            
            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, error = ex.Message });
        }
    }

    // Classes para os requests
    public class AtualizarMensagemRequest
    {
        public int id { get; set; }
        public string novoTexto { get; set; }
    }

    public class ExcluirMensagemRequest
    {
        public int id { get; set; }
    }
}
