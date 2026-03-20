using NF.DTOs.Peca;
using NF.Models;
using NF.Repositories.Interfaces;
using NF.Services.Interfaces;

namespace NF.Services
{
    public class PecaService : IPecaService
    {
        private readonly IPecaRepository _repository;

        public PecaService(IPecaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PecaResponseDTO>> GetAll()
        {
            var pecas = await _repository.GetAll();
            return pecas.Select(p => MapToResponse(p)).ToList();
        }

        public async Task<PecaResponseDTO?> GetById(int id)
        {
            var peca = await _repository.GetById(id);
            if (peca == null) return null;
            return MapToResponse(peca);
        }

        public async Task<List<PecaResponseDTO>> GetByNome(string nome)
        {
            var pecas = await _repository.GetByNome(nome);
            return pecas.Select(p => MapToResponse(p)).ToList();
        }

        public async Task<PecaResponseDTO> GetByCod(string cod)
        {
            var peca = await _repository.GetByCodigo(cod);
            if (peca == null) return null;
            return MapToResponse(peca);
        }

        public async Task<PecaResponseDTO> Create(PecaRequestDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.CodPeca) && await _repository.CodigoExiste(dto.CodPeca))
                throw new Exception("Código de peça já cadastrado.");

            var peca = new Peca
            {
                NomePeca = dto.NomePeca,
                CodPeca = dto.CodPeca,
                PrecoUnitario = dto.PrecoUnitario,
                QtdEstoque = dto.QtdEstoque,
                Fornecedor = dto.Fornecedor
            };

            await _repository.Add(peca);
            return MapToResponse(peca);
        }

        public async Task<PecaResponseDTO?> Update(int id, PecaRequestDTO dto)
        {
            var peca = await _repository.GetById(id);
            if (peca == null) return null;

            if (!string.IsNullOrEmpty(dto.CodPeca) && peca.CodPeca != dto.CodPeca && await _repository.CodigoExiste(dto.CodPeca))
                throw new Exception("Código de peça já cadastrado.");

            peca.NomePeca = dto.NomePeca;
            peca.CodPeca = dto.CodPeca;
            peca.PrecoUnitario = dto.PrecoUnitario;
            peca.QtdEstoque = dto.QtdEstoque;
            peca.Fornecedor = dto.Fornecedor;

            await _repository.Update(peca);
            return MapToResponse(peca);
        }

        public async Task<bool> Delete(int id)
        {
            var peca = await _repository.GetById(id);
            if (peca == null) return false;

            await _repository.Delete(id);
            return true;
        }

        private PecaResponseDTO MapToResponse(Peca peca)
        {
            return new PecaResponseDTO
            {
                IdPeca = peca.IdPeca,
                NomePeca = peca.NomePeca,
                CodPeca = peca.CodPeca,
                PrecoUnitario = peca.PrecoUnitario,
                QtdEstoque = peca.QtdEstoque,
                Fornecedor = peca.Fornecedor
            };
        }
    }
}