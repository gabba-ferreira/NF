using NF.DTOs.Cliente;
using NF.DTOs.OrdemServico;
using NF.Models;

namespace NF.Services.Interfaces
{
    public interface IOrdemServicoService
    {
        Task<List<OrdemServicoResponseDTO>> GetAll();
        Task<OrdemServicoResponseDTO?> GetById(int id);
        Task<List<OrdemServicoResponseDTO>> GetByCliente(int idCliente);
        Task<List<OrdemServicoResponseDTO>> GetByVeiculo(int idVeiculo);
        Task<List<OrdemServicoResponseDTO>> GetByStatus(Status status);
        Task<OrdemServicoResponseDTO> Create(OrdemServicoRequestDTO dto);
        Task<OrdemServicoResponseDTO> CreateComPeca(OrdemServicoRequestDTO dto);
        Task<OrdemServicoResponseDTO?> Update(int id, OrdemServicoRequestDTO dto);
        Task<OrdemServicoResponseDTO?> UpdateStatus(int id, Status status);
        Task<bool> Delete(int id);
    }
}