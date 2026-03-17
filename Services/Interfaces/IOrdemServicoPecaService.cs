using NF.Models;

namespace NF.Services.Interfaces
{
    public interface IOrdemServicoPecaService
    {
        Task<List<OrdemServico_Peca>> GetByOs(int idOs);
        Task Add(OrdemServico_Peca entity);
        Task Update(OrdemServico_Peca entity);
        Task Delete(int idOs, int idPeca);
    }
}
