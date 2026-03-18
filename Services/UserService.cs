using NF.DTOs.User;
using NF.Models;
using NF.Repositories.Interfaces;
using NF.Services.Interfaces;

namespace NF.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserResponseDTO>> GetAll()
        {
            var users = await _repository.GetAll();
            return users.Select(u => MapToResponse(u)).ToList();
        }

        public async Task<UserResponseDTO?> GetById(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null) return null;
            return MapToResponse(user);
        }

        public async Task<UserResponseDTO> Create(UserRequestDTO dto)
        {
            if (await _repository.EmailExiste(dto.Email))
                throw new Exception("E-mail já cadastrado.");


            var user = new User
            {
                NomeCompleto = dto.NomeCompleto,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Telefone = dto.Telefone,
                Role = dto.Role
            };

            await _repository.Add(user);
            return MapToResponse(user);
        }

        public async Task<UserResponseDTO?> Update(int id, UserRequestDTO dto)
        {
            var user = await _repository.GetById(id);
            if (user == null) return null;

            if (user.Email != dto.Email && await _repository.EmailExiste(dto.Email))
                throw new Exception("E-mail já cadastrado.");

            user.NomeCompleto = dto.NomeCompleto;
            user.Email = dto.Email;
            user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            user.Telefone = dto.Telefone;
            user.Role = dto.Role;

            await _repository.Update(user);
            return MapToResponse(user);
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null) return false;

            await _repository.Delete(id);
            return true;
        }

        private UserResponseDTO MapToResponse(User user)
        {
            return new UserResponseDTO
            {
                IdUser = user.IdUser,
                NomeCompleto = user.NomeCompleto,
                Email = user.Email,
                Telefone = user.Telefone,
                Role = (Role)user.Role
            };
        }
    }
}