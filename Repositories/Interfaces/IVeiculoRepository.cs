using NF.Models;

namespace NF.Repositories.Interfaces
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
        Task<List<Veiculo>> GetByCliente(int idCliente);
        Task<Veiculo?> GetByPlaca(string placa);
        Task<bool> PlacaExiste(string placa);

    }
}
