using NF.Models;

namespace NF.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmail(string email);
        Task<bool> EmailExiste(string email);
    }
}
