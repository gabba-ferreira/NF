using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NF.Models
{
    [Table("OrdemServico_Pecas")]
    public class OrdemServico_Peca
    {
        [ForeignKey("Peca")]
        public int IdPeca { get; set; }

        [ForeignKey("OrdemServico")]
        public int IdOs { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        public int QtdPeca { get; set; }

        // Navegação
        public Peca Peca { get; set; } = null!;
        public OrdemServico OrdemServico { get; set; } = null!;
    }
}