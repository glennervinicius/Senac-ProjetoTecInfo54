using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chamados54WebApp.Data
{
    [Table("Chamados")]
    public class Chamado
    {
        //propriedades
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código")]
        public int Id { get; set; }

        //relacionamento 1-N
        [Display(Name = "Cliente")]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        //relacionamento 1-N
        [Display(Name = "Técnico")]
        [ForeignKey("Tecnico")]
        public int IdTecnico{ get; set; }
        public Tecnico Tecnico { get; set; }

        [Required(ErrorMessage = "Data de solicitação obrigatória")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de solicitação")]
        public DateTime DataSolicitacao{ get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Ocorrência")]
        public string? Ocorrencia { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string? Problema { get; set; }

        [Display(Name = "Concluído")]
        public bool Concluido { get; set; }

        public Chamado()
        {
            DataSolicitacao = DateTime.Now;
            Concluido = false;
        }

        //Relacionamento 1-N
        public ICollection<ChamadoServico> ChamadosServicos { get; set; }
    }
}
