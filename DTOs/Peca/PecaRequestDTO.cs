using System.ComponentModel.DataAnnotations;

namespace NF.DTOs.Peca
{
    public class PecaRequestDTO
    {
        [Required(ErrorMessage = "Nome da peça é obrigatório.")]
        [MaxLength(200)]
        public string NomePeca { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? CodPeca { get; set; }

        [Required(ErrorMessage = "Preço unitário é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Quantidade em estoque é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo.")]
        public int QtdEstoque { get; set; }

        [MaxLength(250)]
        public string? Fornecedor { get; set; }
    }
}