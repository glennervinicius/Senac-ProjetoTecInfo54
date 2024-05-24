using Microsoft.AspNetCore.Mvc;

namespace Chamados54WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public abstract class AdminController : Controller
    {

        IWebHostEnvironment servidorWeb;

        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            servidorWeb = webHostEnvironment;
        }

        //metodos para manipulação de arquivos
        public string SalvaArquivo(IFormFile arquivo)
        {
            //verificar se é um arquivo valido
            if (arquivo == null)
            {
                return string.Empty;
            }

            //armazenar o arquivo no servidor web
            var nomeArquivo = $"{Path.GetRandomFileName()}{Path.GetExtension(arquivo.FileName)}";
            var pastaArquivo = Path.Combine(servidorWeb.WebRootPath, "arquivos");
            var localArquivo = Path.Combine(pastaArquivo, nomeArquivo);
            var dadosArquivo = new FileStream(localArquivo, FileMode.Create);
            arquivo.CopyTo(dadosArquivo);
            return nomeArquivo;
        }

        public bool ExcluiArquivo(string nomeArquivo)
        {
            //verifica o nome do arquivo
            if (string.IsNullOrWhiteSpace(nomeArquivo))
            {
                return false;
            }

            //remover o arquivo no servidor web
            var pastaArquivo = Path.Combine(servidorWeb.WebRootPath, "arquivos");
            var localArquivo = Path.Combine(pastaArquivo, nomeArquivo);
            System.IO.File.Delete(localArquivo);
            return true;
        }
        
    }
}
