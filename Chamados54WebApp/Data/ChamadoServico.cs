using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chamados54WebApp.Data
{
    [Table("ChamadosServicos")]
    public class ChamadoServico
    {
        //PK
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código")]
        public int Id { get; set; }

        //FK Chamado
        [Required(ErrorMessage = "Chamado obrigatório")]
        [Display(Name = "Chamado")]
        public int IdChamado { get; set; }
        //Relacionamento FK
        [ForeignKey("IdChamado")]
        public Chamado Chamado { get; set; }

        //FK Servico
        [Required(ErrorMessage = "Serviço obrigatório")]
        [Display(Name = "Serviço")]
        public int IdServico { get; set; }
        //Relacionamento FK
        [ForeignKey("IdServico")]
        public Servico Servico { get; set; }

        public int Quantidade { get; set; }

        [Display(Name = "Valor unitário")]
        public double ValorUnitario { get; set; }

        [Display(Name = "Valor total")]
        public double ValorTotal { get; set; }

        public ChamadoServico()
        {
            Quantidade = 0;
            ValorUnitario = 0;
            ValorTotal = 0;
        }
    }
}
