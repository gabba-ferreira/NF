using NF.Models;

namespace NF.DTOs.User
{
    public class UserResponseDTO
    {
        public int IdUser { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public Role Role { get; set; }
    }
}