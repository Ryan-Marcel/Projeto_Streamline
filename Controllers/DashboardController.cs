using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Dotnet8.Data;
using Projeto_Dotnet8.Models;

namespace Projeto_Dotnet8.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly BancoContext _context;

        public DashboardController(BancoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // API para gráfico de pizza (Status das notificações)
        [HttpGet]
        public IActionResult GetStatusData()
        {
            try
            {
                var statusData = _context.Mensagens
                    .GroupBy(m => m.Status)
                    .Select(g => new
                    {
                        status = g.Key,
                        count = g.Count()
                    })
                    .ToList();

                var result = new
                {
                    labels = statusData.Select(s => s.status == 0 ? "Em Aberto" : 
                                                    s.status == 1 ? "Em Andamento" : "Resolvido").ToArray(),
                    data = statusData.Select(s => s.count).ToArray()
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // API para gráfico de linha (Notificações por mês)
        [HttpGet]
        public IActionResult GetMonthlyData()
        {
            try
            {
                var sixMonthsAgo = DateTime.Now.AddMonths(-6);
                
                var monthlyData = _context.Mensagens
                    .Where(m => m.DataCriacao >= sixMonthsAgo)
                    .GroupBy(m => new { m.DataCriacao.Year, m.DataCriacao.Month })
                    .Select(g => new
                    {
                        year = g.Key.Year,
                        month = g.Key.Month,
                        count = g.Count()
                    })
                    .OrderBy(x => x.year)
                    .ThenBy(x => x.month)
                    .ToList();

                var result = new
                {
                    labels = monthlyData.Select(m => $"{GetMonthName(m.month)}/{m.year}").ToArray(),
                    data = monthlyData.Select(m => m.count).ToArray()
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // API para gráfico de barras (Solicitações por sala)
        [HttpGet]
        public IActionResult GetSalaData()
        {
            try
            {
                var salaData = _context.Mensagens
                    .Include(m => m.Computador)
                    .GroupBy(m => m.Computador.SalaModelsID)
                    .Select(g => new
                    {
                        salaId = g.Key,
                        count = g.Count()
                    })
                    .ToList();

                var salas = _context.Salas
                    .Where(s => salaData.Select(sd => sd.salaId).Contains(s.ID))
                    .ToList();

                var result = new
                {
                    labels = salas.Select(s => s.Sala_Num).ToArray(),
                    data = salaData.Select(sd => sd.count).ToArray()
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

         [HttpGet]
        public IActionResult GetTopDefectiveComputers()
        {
            try
            {
                var topComputers = _context.Mensagens
                    .Include(m => m.Computador)
                    .GroupBy(m => new { m.ComputadorID, m.Computador.Nome })
                    .Select(g => new
                    {
                        computadorId = g.Key.ComputadorID,
                        computadorNome = g.Key.Nome,
                        totalSolicitacoes = g.Count(),
                        emAberto = g.Count(m => m.Status == 0),
                        emAndamento = g.Count(m => m.Status == 1),
                        resolvidos = g.Count(m => m.Status == 2)
                    })
                    .OrderByDescending(c => c.totalSolicitacoes)
                    .Take(5)
                    .ToList();

                return Json(topComputers);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        private string GetMonthName(int month)
        {
            return month switch
            {
                1 => "Jan",
                2 => "Fev",
                3 => "Mar",
                4 => "Abr",
                5 => "Mai",
                6 => "Jun",
                7 => "Jul",
                8 => "Ago",
                9 => "Set",
                10 => "Out",
                11 => "Nov",
                12 => "Dez",
                _ => ""
            };
        }
    }
}

