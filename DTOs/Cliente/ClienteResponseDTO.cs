namespace NF.DTOs.Cliente
{
    public class ClienteResponseDTO
    {
        public int IdCliente { get; set; }
        public string RazaoSocial { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string? TelefoneOp { get; set; }
        public string Endereco { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
    }
}