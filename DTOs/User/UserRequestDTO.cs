using System.ComponentModel.DataAnnotations;
using NF.Models;

namespace NF.DTOs.User
{
    public class UserRequestDTO
    {
        [Required(ErrorMessage = "Nome completo é obrigatório.")]
        [MaxLength(200)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres.")]
        [MaxLength(255)]
        public string Senha { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Role é obrigatória.")]
        public Role? Role { get; set; }
    }
}