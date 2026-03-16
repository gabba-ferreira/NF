using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NF.Models
{
    [Table("Pecas")]
    public class Peca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPeca { get; set; }

        [Required(ErrorMessage = "Nome da peça é obrigatório.")]
        [MaxLength(200)]
        public string NomePeca { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? CodPeca { get; set; }

        [Required(ErrorMessage = "Preço unitário é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero.")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Quantidade em estoque é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo.")]
        public int QtdEstoque { get; set; }

        [MaxLength(250)]
        public string? Fornecedor { get; set; }

        public ICollection<OrdemServico_Peca> OrdemServico_Pecas { get; set; } = new List<OrdemServico_Peca>();
    }
}