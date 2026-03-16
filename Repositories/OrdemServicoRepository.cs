using Microsoft.EntityFrameworkCore;
using NF.Data;
using NF.Models;
using NF.Repositories.Interfaces;

namespace NF.Repositories
{
    public class OrdemServicoRepository : BaseRepository<OrdemServico>, IOrdemServicoRepository
    {
        public OrdemServicoRepository(NFContext context) : base(context)
        {
        }

        public async Task<List<OrdemServico>> GetByCliente(int idCliente)
        {
            return await _dbSet
                .Where(o => o.IdCliente == idCliente)
                .Include(o => o.Cliente)
                .Include(o => o.Veiculo)
                .ToListAsync();
        }

        public async Task<OrdemServico?>GetByIdWithDetails(int idOs)
        {
            return await _dbSet
            .Include(o => o.Cliente)
            .Include(o => o.Veiculo)
            .Include(o => o.OrdemServico_Pecas)
                .ThenInclude(op => op.Peca)
            .FirstOrDefaultAsync(o => o.IdOs == idOs);
        }

        public async Task<List<OrdemServico>> GetByStatus(Status status)
        {
            return await _dbSet
                 .Where(o => o.Status == status)
                 .Include(o => o.Cliente)
                 .Include(o => o.Veiculo)
                 .ToListAsync();
        }

        public async Task<List<OrdemServico>> GetByVeiculo(int idVeiculo)
        {
            return await _dbSet
            .Where(o => o.IdVeiculo == idVeiculo)
            .Include(o => o.Cliente)
            .Include(o => o.Veiculo)
            .ToListAsync();
        }
    }
}
