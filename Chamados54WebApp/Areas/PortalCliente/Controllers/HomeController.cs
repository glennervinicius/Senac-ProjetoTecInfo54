using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Chamados54WebApp.Data;
using System.Security.Claims;

namespace Chamados54WebApp.Areas.PortalCliente.Controllers
{
    [Area("PortalCliente")]
    public class HomeController : Controller
    {

        private readonly BancoDados _context;

        public HomeController()
        {
            _context = new BancoDados();
        }

        private int getIdCliente()
        {
            return Int32.Parse(User.Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).ToList()[0].Value);
        }

        // GET: PortalCliente/Home
        public async Task<IActionResult> Index()
        {
            var id = getIdCliente();
            var bancoDados = _context.Chamados
                .Where(c => c.IdCliente == id)
                .Include(c => c.Cliente)
                .Include(c => c.Tecnico)
                .OrderByDescending(o => o.Id);

            return View(await bancoDados.ToListAsync());
        }

        // GET: PortalCliente/Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chamados == null)
            {
                return NotFound();
            }

            var chamado = await _context.Chamados
                .Include(c => c.Cliente)
                .Include(c => c.Tecnico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chamado == null)
            {
                return NotFound();
            }

            return View(chamado);
        }

        // GET: PortalCliente/Home/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Nome");
            ViewData["IdTecnico"] = new SelectList(_context.Tecnicos, "Id", "Nome");
            return View();
        }

        // POST: PortalCliente/Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ocorrencia")] Chamado chamado)
        {
            chamado.IdCliente = getIdCliente();
            chamado.Concluido = false;
            chamado.DataSolicitacao = DateTime.Now;

            // atualizando model
            await TryUpdateModelAsync(chamado);

            _context.Add(chamado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: PortalCliente/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chamados == null)
            {
                return NotFound();
            }

            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null)
            {
                return NotFound();
            }

            chamado.Concluido = true;

            _context.Update(chamado);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: PortalCliente/Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Chamados == null)
            {
                return Problem("Entity set 'BancoDados.Chamados'  is null.");
            }
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado != null)
            {
                _context.Chamados.Remove(chamado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> NaoResolvido(int id)
        {
            var chamado = _context.Chamados.First(w => w.Id == id);
            chamado.Ocorrencia = chamado.Ocorrencia + "; [Atualização "+DateTime.Now.ToShortDateString()+"] > " + Request.Query["texto"];
            chamado.Problema = null;

            _context.Update(chamado);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool ChamadoExists(int id)
        {
          return (_context.Chamados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
