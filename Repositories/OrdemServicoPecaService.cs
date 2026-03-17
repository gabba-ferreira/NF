using NF.Models;
using NF.Repositories.Interfaces;
using NF.Services.Interfaces;

namespace NF.Repositories
{
    public class OrdemServicoPecaService : IOrdemServicoPecaService
    {
        private readonly IOrdemServicoPecaRepository _repository;
        private readonly I
        public OrdemServicoPecaService()
        {
            
        }
        public Task Add(OrdemServico_Peca entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int idOs, int idPeca)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrdemServico_Peca>> GetByOs(int idOs)
        {
            throw new NotImplementedException();
        }

        public Task Update(OrdemServico_Peca entity)
        {
            throw new NotImplementedException();
        }
    }
}
