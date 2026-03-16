namespace NF.DTOs.OrdemServico_Peca
{
    public class OrdemServicoPecaResponseDTO
    {
        public int IdOs { get; set; }
        public int IdPeca { get; set; }
        public string NomePeca { get; set; } = string.Empty;
        public string? CodPeca { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int QtdPeca { get; set; }
        public decimal ValorTotal { get; set; }
    }
}