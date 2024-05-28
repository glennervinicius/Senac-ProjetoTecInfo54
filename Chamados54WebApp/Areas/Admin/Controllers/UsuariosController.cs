using Chamados54WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chamados54WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : AdminController
    {
        //globais
        BancoDados bancoDados;

        public UsuariosController(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            //inicializa o banco de dados
            bancoDados = new BancoDados();
            //lista todos os usuarios
            var usuarios = bancoDados.Usuarios.ToList();
            //envia a lista de usuarios para a view
            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string busca)
        {
            //inicializa o banco de dados
            bancoDados = new BancoDados();
            //lista os usuarios realizando a busca
            var usuarios = new List<Usuario>();
            if (string.IsNullOrWhiteSpace(busca))
            {
                usuarios = bancoDados.Usuarios.ToList();
            }
            else
            {
                usuarios = bancoDados.Usuarios.Where(e => e.Email.Contains(busca)).ToList();
            }
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Inclui()
        {
            Usuario usuario = new Usuario();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inclui(Usuario usuario, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                //verifica se existe um arquivo
                if (arquivo != null)
                {
                    var nomeArquivo = SalvaArquivo(arquivo);
                    usuario.Foto = nomeArquivo;
                }

                //inclui o usuario no banco de dados
                bancoDados = new BancoDados();
                bancoDados.Usuarios.Add(usuario); //inclui
                bancoDados.SaveChanges();  //salva
                //voltar para index
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Altera(int id)
        {
            //obtem usuario do banco de dados
            bancoDados = new BancoDados();
            var usuario = bancoDados.Usuarios.FirstOrDefault(e => e.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Altera(Usuario usuario, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                //verifica se existe um arquivo
                if (arquivo != null)
                {
                    //exclui o arquivo antigo
                    ExcluiArquivo(usuario.Foto);
                    //salva o arquivo novo
                    var nomeArquivo = SalvaArquivo(arquivo);
                    usuario.Foto = nomeArquivo;
                }
                //altera o usuario no banco de dados
                bancoDados = new BancoDados();
                bancoDados.Usuarios.Update(usuario);
                bancoDados.SaveChanges();
                //volta para index
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Exibe(int id)
        {
            //obtem usuario do banco de dados
            bancoDados = new BancoDados();
            var usuario = bancoDados.Usuarios.FirstOrDefault(e => e.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Exclui(int id)
        {
            //obtem usuario do banco de dados
            bancoDados = new BancoDados();
            var usuario = bancoDados.Usuarios.FirstOrDefault(e => e.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Exclui(Usuario usuario)
        {
            if (usuario.Id > 0)
            {
                //exclui usuario do banco de dados
                using (var db = new BancoDados())
                {
                    var usuarioDB = db.Usuarios.Where(w => w.Id == usuario.Id)
                        .Include(a => a.Cliente)
                        .Include(a => a.Tecnico)
                        .First();

                    var ticketsTecnico = db.Chamados.Where(c => usuarioDB.Tecnico != null && c.IdTecnico == usuario.Id)
                    .ToList();
                    foreach (var item in ticketsTecnico)
                    {
                        item.Tecnico = null;
                        db.Chamados.Update(item);
                    };

                    var ticketsClienteApagar = db.Chamados.Where(c => usuarioDB.Cliente != null && c.IdCliente == usuario.Id)
                        .ToList();

                    foreach (var item in ticketsClienteApagar)
                    {
                        db.Chamados.Remove(item);
                    }
                    db.SaveChanges();
                }

                using (var db = new BancoDados())
                {
                    var tec = db.Tecnicos.Where(w => w.Id == usuario.Id).FirstOrDefault();
                    var cli = db.Clientes.Where(w => w.Id == usuario.Id).FirstOrDefault();
                    var usr = db.Usuarios.Where(w => w.Id == usuario.Id).FirstOrDefault();

                    if (tec != null && tec.Id > 0)
                        db.Tecnicos.Remove(tec);
                    if (cli != null && cli.Id > 0)
                        db.Clientes.Remove(cli);
                    if (usr != null && usr.Id > 0)
                        db.Usuarios.Remove(usr);

                    db.SaveChanges();
                }



                //volta para index
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

    }
}
