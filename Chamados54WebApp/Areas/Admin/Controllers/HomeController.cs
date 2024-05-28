using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Chamados54WebApp.Data;
using Chamados54WebApp.Areas.Admin.Models;

namespace Chamados54WebApp.Areas.Admin.Controllers
{    
    [Authorize(Roles = "Administrador,Tecnico,Cliente")]
    public class HomeController : AdminController
    {
        public BancoDados _context { get; private set; }
        public HomeController(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
        {
            _context = new BancoDados();
        }

        public IActionResult Index()
        {
            var dadosDashboard = new Dashboard
            {
                CadastroQuantidadeCliente = _context.Clientes.Count(),
                CadastroQuantidadeTecnico = _context.Tecnicos.Count(),

                ChamadoAguardandoAceiteTecnico = _context.Chamados.Where(f => f.Tecnico == null && !f.Concluido).Count(),
                ChamadoAguardandoCliente = _context.Chamados.Where(f => !string.IsNullOrEmpty(f.Problema) && !f.Concluido).Count(),
                ChamadoAguardandoTecnico = _context.Chamados.Where(f => string.IsNullOrEmpty(f.Problema) && !f.Concluido).Count(),
                ChamadoFinalizado = _context.Chamados.Where(f => f.Concluido).Count(),
            };

            return View(dadosDashboard);
        }
    }
}
