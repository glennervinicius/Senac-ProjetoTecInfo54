using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chamados54WebApp.Data
{
    [Table("Clientes")]
    public class Cliente
    {
        //relacionamento 1-1
        [ForeignKey("Usuario")]
        [Display(Name = "Usuário")]
        public int Id { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessage ="Nome obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(100)]
        [Display(Name = "Profissão")]
        public string? Profissao { get; set; }

        [StringLength(100)]
        public string? Setor { get; set; }

        //relacionamento 1-N
        public ICollection<Chamado> Chamados { get; set; }
    }
}
