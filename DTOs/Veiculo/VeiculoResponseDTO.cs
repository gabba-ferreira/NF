namespace NF.DTOs.Veiculo
{
    public class VeiculoResponseDTO
    {
        public int IdVeiculo { get; set; }
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string PlacaVeiculo { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public int Horimetro { get; set; }
    }
}