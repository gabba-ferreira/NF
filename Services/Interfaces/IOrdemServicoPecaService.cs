using NF.DTOs.OrdemServico_Peca;
using NF.Models;

namespace NF.Services.Interfaces
{
    public interface IOrdemServicoPecaService
    {
        Task<List<OrdemServicoPecaResponseDTO>> GetByOs(int idOs);
        Task <OrdemServicoPecaResponseDTO> Add(int idOs, OrdemServicoPecaRequestDTO dto);
        Task <OrdemServicoPecaResponseDTO> Update(int idOs, int idPeca, int qtdPeca);
        Task<bool> Delete(int idOs, int idPeca);
    }
}
