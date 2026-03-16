using System.ComponentModel.DataAnnotations;

namespace NF.DTOs.Cliente
{
    public class ClienteRequestDTO
    {
        [Required(ErrorMessage = "Razão Social é obrigatória.")]
        [MaxLength(200)]
        public string RazaoSocial { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "CNPJ é obrigatório.")]
        [MaxLength(100)]
        public string CNPJ { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório.")]
        [MaxLength(100)]
        public string Telefone { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? TelefoneOp { get; set; }

        [Required(ErrorMessage = "Endereço é obrigatório.")]
        [MaxLength(250)]
        public string Endereco { get; set; } = string.Empty;

        [Required(ErrorMessage = "CEP é obrigatório.")]
        [MaxLength(100)]
        public string CEP { get; set; } = string.Empty;
    }
}