using Microsoft.EntityFrameworkCore;
using NF.Data;
using NF.Models;
using NF.Repositories.Interfaces;

namespace NF.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {

        public ClienteRepository(NFContext context) : base(context)
        {
            
        }
        public async Task<bool> CNPJExiste(string cnpj)
        {
            return await _dbSet.AnyAsync(c => c.CNPJ == cnpj);
        }

        public async Task<Cliente?> GetByCNPJ(string cnpj)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.CNPJ == cnpj);
        }

        public async Task<List<Cliente>> GetByRazaoSocial(string razaoSocial)
        {
            return await _dbSet
                .Where(c => c.RazaoSocial.Contains(razaoSocial))
                .ToListAsync();
        }
    }
}
