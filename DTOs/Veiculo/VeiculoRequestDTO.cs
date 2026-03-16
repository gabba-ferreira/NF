using System.ComponentModel.DataAnnotations;

public class VeiculoRequestDTO
{
    [Required(ErrorMessage = "IdCliente é obrigatório.")]
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
    public int Horimetro { get; set; }
}