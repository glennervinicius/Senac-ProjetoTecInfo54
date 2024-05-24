using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chamados54WebApp.Data
{
    [Table("Servicos")]
    public class Servico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(1000)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [DataType(DataType.Currency)]
        public double Valor { get; set; }

        public Servico()
        {
            Valor = 0;             
        }

        //Relacionamento 1-N
        public ICollection<ChamadoServico> ChamadosServicos { get; set; }
    }
}
