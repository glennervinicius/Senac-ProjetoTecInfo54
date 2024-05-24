using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chamados54WebApp.Data
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage ="E-mail obrigatório")]
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

        [DataType(DataType.ImageUrl)]
        public string? Foto { get; set; }

        //relacionamento 1-1
        [ValidateNever]
        public virtual Cliente Cliente { get; set; }
        [ValidateNever]
        public virtual Tecnico Tecnico { get; set; }

    }

    public enum PerfilUsuario
    {
        Cliente = 0,
        Tecnico = 1,
        Administrador = 2,
    }
}
