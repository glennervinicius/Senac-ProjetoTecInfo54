using Chamados54WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                if (arquivo !=null)
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
                bancoDados = new BancoDados();
                bancoDados.Usuarios.Remove(usuario);
                bancoDados.SaveChanges();
                //volta para index
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

    }
}
