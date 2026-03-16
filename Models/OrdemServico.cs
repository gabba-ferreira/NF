using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NF.Models
{
    public enum Status
    {
        Aberto = 1,
        AguardandoPagamento = 2,
        Finalizado = 3,
        Cancelado = 4
    }

    [Table("OrdensServico")]
    public class OrdemServico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOs { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [ForeignKey("Veiculo")]
        public int IdVeiculo { get; set; }

        [Required(ErrorMessage = "Status é obrigatório.")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Responsável é obrigatório.")]
        [MaxLength(250)]
        public string Responsavel { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? DefeitoDesc { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Valor não pode ser negativo.")]
        public decimal? ValorMaoDeObra { get; set; }

        public DateTime? DtVisita { get; set; }

        public DateTime? DtFim { get; set; }

        // Navegação
        public Cliente Cliente { get; set; } = null!;
        public Veiculo Veiculo { get; set; } = null!;
        public ICollection<OrdemServico_Peca> OrdemServico_Pecas { get; set; } = new List<OrdemServico_Peca>();
    }
}