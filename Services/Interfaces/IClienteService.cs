using NF.DTOs.Cliente;

namespace NF.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteResponseDTO>> GetAll();
        Task<ClienteResponseDTO?> GetById(int id);
        Task<List<ClienteResponseDTO>> GetByRazaoSocial(string razaoSocial);
        Task<ClienteResponseDTO> Create(ClienteRequestDTO dto);
        Task<ClienteResponseDTO?> Update(int id, ClienteRequestDTO dto);
        Task<bool> Delete(int id);
    }
}