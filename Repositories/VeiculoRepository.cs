using Microsoft.EntityFrameworkCore;
using NF.Data;
using NF.Models;
using NF.Repositories.Interfaces;

namespace NF.Repositories
{
    public class VeiculoRepository : BaseRepository<Veiculo>, IVeiculoRepository
    {

        public VeiculoRepository(NFContext context) : base(context)
        {
            
        }
        public async Task<List<Veiculo>> GetByCliente(int idCliente)
        {
            return await _dbSet
                 .Where(v => v.IdCliente == idCliente)
                 .Include(v => v.Cliente)
                 .ToListAsync();
        }

        public async Task<Veiculo?> GetByPlaca(string placa)
        {
            return await _dbSet
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.PlacaVeiculo == placa);

        }

        public async Task<bool> PlacaExiste(string placa)
        {
            return await _dbSet
             .AnyAsync(v => v.PlacaVeiculo == placa);
        }
    }
}
