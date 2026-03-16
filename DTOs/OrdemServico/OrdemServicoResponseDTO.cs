using NF.Models;

namespace NF.DTOs.OrdemServico
{
    public class OrdemServicoResponseDTO
    {
        public int IdOs { get; set; }
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public int IdVeiculo { get; set; }
        public string ModeloVeiculo { get; set; } = string.Empty;
        public string PlacaVeiculo { get; set; } = string.Empty;
        public Status Status { get; set; }
        public string Responsavel { get; set; } = string.Empty;
        public string? DefeitoDesc { get; set; }
        public decimal? ValorMaoDeObra { get; set; }
        public DateTime? DtVisita { get; set; }
        public DateTime? DtFim { get; set; }
    }
}