namespace NF.DTOs.Peca
{
    public class PecaResponseDTO
    {
        public int IdPeca { get; set; }
        public string NomePeca { get; set; } = string.Empty;
        public string? CodPeca { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int QtdEstoque { get; set; }
        public string? Fornecedor { get; set; }
    }
}