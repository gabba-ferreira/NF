using NF.Models;

namespace NF.Repositories.Interfaces
{
    public interface IOrdemServicoPecaRepository
    {
        Task<List<OrdemServico_Peca>> GetByOs(int idOs);
        Task<OrdemServico_Peca?> GetByOsAndPeca(int idOs, int idPeca);
        Task Add(OrdemServico_Peca entity);
        Task Update(OrdemServico_Peca entity);
        Task Delete(int idOs, int idPeca);
    }
}