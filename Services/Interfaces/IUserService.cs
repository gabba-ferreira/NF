using NF.DTOs.User;

namespace NF.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetAll();
        Task<UserResponseDTO?> GetById(int id);
        Task<UserResponseDTO> Create(UserRequestDTO dto);
        Task<UserResponseDTO?> Update(int id, UserRequestDTO dto);
        Task<bool> Delete(int id);
    }
}