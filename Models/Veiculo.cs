using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NF.Models
{
    [Table("Veiculos")]
    public class Veiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVeiculo { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Modelo é obrigatório.")]
        [MaxLength(250)]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Placa é obrigatória.")]
        [MaxLength(250)]
        public string PlacaVeiculo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Marca é obrigatória.")]
        [MaxLength(250)]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "Horímetro é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")]
        public int Horimetro { get; set; }

        public Cliente Cliente { get; set; } = null!;
        public ICollection<OrdemServico> OrdemServicos { get; set; } = new List<OrdemServico>();
    }
}