using Microsoft.EntityFrameworkCore;
using NF.Data;
using NF.Models;
using NF.Repositories.Interfaces;

namespace NF.Repositories
{
    public class PecaRepository : BaseRepository<Peca>, IPecaRepository
    {
        public PecaRepository(NFContext context) : base(context)
        {
        }

        public async Task<Peca?> GetByCodigo(string codPeca)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.CodPeca == codPeca);
        }

        public async Task<bool> CodigoExiste(string codPeca)
        {
            return await _dbSet
                .AnyAsync(p => p.CodPeca == codPeca);
        }

        public async Task<List<Peca>> GetByNome(string nomePeca)
        {
            return await _dbSet
                .Where(p => p.NomePeca.Contains(nomePeca))
                .ToListAsync();
        }

    }
}