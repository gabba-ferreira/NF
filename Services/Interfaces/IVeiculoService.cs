using NF.DTOs.Veiculo;

namespace NF.Services.Interfaces
{
    public interface IVeiculoService
    {
        Task<List<VeiculoResponseDTO>> GetAll();
        Task<VeiculoResponseDTO?> GetById(int id);
        Task<List<VeiculoResponseDTO>> GetByCliente(int idCliente);
        Task<VeiculoResponseDTO?> GetByPlaca(string placa);
        Task<VeiculoResponseDTO> Create(VeiculoRequestDTO dto);
        Task<VeiculoResponseDTO?> Update(int id, VeiculoRequestDTO dto);
        Task<bool> Delete(int id);
    }
}