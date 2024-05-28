using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chamados54WebApp.Data;
using System.Security.Claims;

namespace Chamados54WebApp.Areas.PortalTecnico.Controllers
{
    [Area("PortalTecnico")]
    public class HomeController : Controller
    {
        private readonly BancoDados _context;

        public HomeController()
        {
            _context = new BancoDados();
        }

        private int getIdTecnico()
        {
            return Int32.Parse(User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).ToList()[0].Value);
        }

        // GET: Tecnico/Home
        public async Task<IActionResult> Index()
        {
            var bancoDados = _context.Chamados
                .Include(c => c.Cliente)
                .Include(c => c.Tecnico)
                .Where(f => f.IdTecnico == getIdTecnico() || (f.IdTecnico == null && !f.Concluido))
                .OrderBy(o => o.Tecnico == null ? 0 : 1)
                .OrderBy(o => o.Concluido ? 0 : 1);

            return View(await bancoDados.ToListAsync());
        }

        public async Task<IActionResult> Aceitar(int? id)
        {
            var chamado = _context.Chamados.First(w => w.Id == id);
            chamado.IdTecnico = getIdTecnico();
            _context.Update(chamado);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Abandonar(int? id)
        {
            var chamado = _context.Chamados.First(w => w.Id == id);
            chamado.IdTecnico = null;
            _context.Update(chamado);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Solucao(int id)
        {   
            var chamado = _context.Chamados.First(w => w.Id == id);
            chamado.Problema = Request.Query["texto"];

            _context.Update(chamado);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
