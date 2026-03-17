using NF.DTOs.Peca;

namespace NF.Services.Interfaces
{
    public interface IPecaService
    {
        Task<List<PecaResponseDTO>> GetAll();
        Task<PecaResponseDTO?> GetById(int id);
        Task<List<PecaResponseDTO>> GetByNome(string nome);
        Task<PecaResponseDTO> GetByCod(string codigo);
        Task<PecaResponseDTO> Create(PecaRequestDTO dto);
        Task<PecaResponseDTO?> Update(int id, PecaRequestDTO dto);
        Task<bool> Delete(int id);
    }
}