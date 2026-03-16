using NF.Models;

namespace NF.Repositories.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> GetByCNPJ(string cnpj);
        Task<bool> CNPJExiste(string cnpj);
        Task<List<Cliente>> GetByRazaoSocial(string razaoSocial);
    }
}
