using NF.Models;

namespace NF.Repositories.Interfaces
{
    public interface IOrdemServicoRepository : IRepository<OrdemServico>
    {
        Task<List<OrdemServico>> GetByCliente(int idCliente);
        Task<List<OrdemServico>> GetByVeiculo(int idVeiculo);
        Task<List<OrdemServico>> GetByStatus(Status status);
        Task<OrdemServico?> GetByIdWithDetails(int idOs);
    }
}