using Microsoft.EntityFrameworkCore;
using NF.Data;
using NF.Models;
using NF.Repositories.Interfaces;

namespace NF.Repositories
{
    public class OrdemServicoPecaRepository : IOrdemServicoPecaRepository
    {
        private readonly NFContext _context;

        public OrdemServicoPecaRepository(NFContext context)
        {
            _context = context;
        }

        public async Task<List<OrdemServico_Peca>> GetByOs(int idOs)
        {
            return await _context.OrdemServico_Pecas
                .Where(op => op.IdOs == idOs)
                .Include(op => op.Peca)
                .ToListAsync();
        }

        public async Task<OrdemServico_Peca?> GetByOsAndPeca(int idOs, int idPeca)
        {
            return await _context.OrdemServico_Pecas
                .Include(op => op.Peca)
                .FirstOrDefaultAsync(op => op.IdOs == idOs && op.IdPeca == idPeca);
        }

        public async Task Add(OrdemServico_Peca entity)
        {
            await _context.OrdemServico_Pecas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(OrdemServico_Peca entity)
        {
            _context.OrdemServico_Pecas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int idOs, int idPeca)
        {
            var entity = await GetByOsAndPeca(idOs, idPeca);
            if (entity != null)
            {
                _context.OrdemServico_Pecas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}