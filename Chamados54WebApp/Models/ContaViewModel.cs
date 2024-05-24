using Chamados54WebApp.Data;
using System.ComponentModel.DataAnnotations;

namespace Chamados54WebApp.Models
{
    public class ContaViewModel
    {
        [Required(ErrorMessage = "Nome obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail obrigatório")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um e-mail válido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        [StringLength(100)]
        [DataType(DataType.Password, ErrorMessage = "Informe uma senha válida")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Perfil obrigatório")]
        [Display(Name = "Perfil do usuário")]
        public PerfilUsuario Perfil { get; set; }
    }
}
