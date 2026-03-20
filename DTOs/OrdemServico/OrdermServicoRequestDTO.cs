using System.ComponentModel.DataAnnotations;
using NF.DTOs.OrdemServico_Peca;
using NF.Models;

namespace NF.DTOs.OrdemServico
{
    public class OrdemServicoRequestDTO
    {
        [Required(ErrorMessage = "IdCliente é obrigatório.")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "IdVeiculo é obrigatório.")]
        public int IdVeiculo { get; set; }

        [Required(ErrorMessage = "Status é obrigatório.")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Responsável é obrigatório.")]
        [MaxLength(250)]
        public string Responsavel { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? DefeitoDesc { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Valor não pode ser negativo.")]
        public decimal? ValorMaoDeObra { get; set; }

        public DateTime? DtVisita { get; set; }
        public DateTime? DtFim { get; set; }

        public List<OrdemServicoPecaRequestDTO> Pecas { get; set; }
    }
}