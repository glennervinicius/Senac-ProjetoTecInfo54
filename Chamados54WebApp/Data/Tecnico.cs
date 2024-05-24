using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chamados54WebApp.Data
{
    [Table("Tecnicos")]
    public class Tecnico
    {
        //relacionamento 1-1
        [ForeignKey("Usuario")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        public virtual Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(100)]
        public string? Especialidade { get; set; }
    }
}
