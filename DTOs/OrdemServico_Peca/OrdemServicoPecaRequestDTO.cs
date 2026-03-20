using System.ComponentModel.DataAnnotations;

namespace NF.DTOs.OrdemServico_Peca
{
    public class OrdemServicoPecaRequestDTO
    {


        [Required(ErrorMessage = "IdPeca é obrigatório.")]
        public int IdPeca { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser pelo menos 1.")]
        public int QtdPeca { get; set; }
    }
}