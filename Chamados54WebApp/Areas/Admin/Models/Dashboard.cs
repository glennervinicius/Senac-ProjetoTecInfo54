namespace Chamados54WebApp.Areas.Admin.Models;

public class Dashboard
{
    public int CadastroQuantidadeCliente { get; set; }
    public int CadastroQuantidadeTecnico { get; set; }
    public int ChamadoAguardandoCliente { get; set; }
    public int ChamadoAguardandoTecnico { get; set; }
    public int ChamadoAguardandoAceiteTecnico { get; set; }
    public int ChamadoFinalizado { get; set; }
}
