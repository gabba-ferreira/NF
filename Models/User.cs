using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NF.Models
{
    public enum Role
    {
        Admin = 1,
        Usuario = 2
    }

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Nome completo é obrigatório!")]
        [MaxLength(200)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MaxLength(255)]
        public string SenhaHash { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Telefone { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}