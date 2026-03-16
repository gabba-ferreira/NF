using NF.Models;

namespace NF.Repositories.Interfaces
{
    public interface IPecaRepository : IRepository<Peca>
    {
        Task<Peca?> GetByCodigo(string codPeca);
        Task<bool> CodigoExiste(string codPeca);
        Task<List<Peca>> GetByNome(string nomePeca);
    }
}
