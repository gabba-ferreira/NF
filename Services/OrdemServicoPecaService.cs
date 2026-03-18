using NF.DTOs.OrdemServico_Peca;
using NF.Models;
using NF.Repositories.Interfaces;
using NF.Services.Interfaces;

namespace NF.Services
{
    public class OrdemServicoPecaService : IOrdemServicoPecaService
    {
        private readonly IOrdemServicoPecaRepository _repository;
        private readonly IOrdemServicoRepository _osRepository;
        private readonly IPecaRepository _pecaRepository;

        public OrdemServicoPecaService(
            IOrdemServicoPecaRepository repository,
            IOrdemServicoRepository osRepository,
            IPecaRepository pecaRepository)
        {
            _repository = repository;
            _osRepository = osRepository;
            _pecaRepository = pecaRepository;
        }

        public async Task<List<OrdemServicoPecaResponseDTO>> GetByOs(int idOs)
        {
            var itens = await _repository.GetByOs(idOs);
            return itens.Select(op => MapToResponse(op)).ToList();
        }

        public async Task<OrdemServicoPecaResponseDTO> Add(int idOs, OrdemServicoPecaRequestDTO dto)
        {
            var os = await _osRepository.GetById(idOs);
            if (os == null)
                throw new Exception("Ordem de Serviço não encontrada.");

            if (os.Status == Status.Finalizado || os.Status == Status.Cancelado)
                throw new Exception("Não é possível adicionar peças a uma OS finalizada ou cancelada.");

            var peca = await _pecaRepository.GetById(dto.IdPeca);
            if (peca == null)
                throw new Exception("Peça não encontrada.");

            if (peca.QtdEstoque < dto.QtdPeca)
                throw new Exception($"Estoque insuficiente. Disponível: {peca.QtdEstoque}.");

            var jaExiste = await _repository.GetByOsAndPeca(dto.IdOs, dto.IdPeca);
            if (jaExiste != null)
                throw new Exception("Peça já adicionada a esta OS. Use a opção de editar para alterar a quantidade.");

            // Desconta do estoque
            peca.QtdEstoque -= dto.QtdPeca;
            await _pecaRepository.Update(peca);

            var item = new OrdemServico_Peca
            {
                IdOs = dto.IdOs,
                IdPeca = dto.IdPeca,
                QtdPeca = dto.QtdPeca
            };

            await _repository.Add(item);
            return MapToResponse(item);
        }

        public async Task<OrdemServicoPecaResponseDTO?> Update(int idOs, int idPeca, int qtdPeca)
        {
            var item = await _repository.GetByOsAndPeca(idOs, idPeca);
            if (item == null) return null;

            var peca = await _pecaRepository.GetById(idPeca);
            if (peca == null) return null;

            // Devolve a quantidade antiga ao estoque e desconta a nova
            peca.QtdEstoque += item.QtdPeca;
            peca.QtdEstoque -= qtdPeca;

            if (peca.QtdEstoque < 0)
                throw new Exception($"Estoque insuficiente. Disponível: {peca.QtdEstoque + qtdPeca}.");

            await _pecaRepository.Update(peca);

            item.QtdPeca = qtdPeca;
            await _repository.Update(item);
            return MapToResponse(item);
        }

        public async Task<bool> Delete(int idOs, int idPeca)
        {
            var item = await _repository.GetByOsAndPeca(idOs, idPeca);
            if (item == null) return false;

            // Devolve ao estoque
            var peca = await _pecaRepository.GetById(idPeca);
            if (peca != null)
            {
                peca.QtdEstoque += item.QtdPeca;
                await _pecaRepository.Update(peca);
            }

            await _repository.Delete(idOs, idPeca);
            return true;
        }

        private OrdemServicoPecaResponseDTO MapToResponse(OrdemServico_Peca item)
        {
            return new OrdemServicoPecaResponseDTO
            {
                IdOs = item.IdOs,
                IdPeca = item.IdPeca,
                NomePeca = item.Peca?.NomePeca ?? string.Empty,
                CodPeca = item.Peca?.CodPeca,
                PrecoUnitario = item.Peca?.PrecoUnitario ?? 0,
                QtdPeca = item.QtdPeca,
                ValorTotal = (item.Peca?.PrecoUnitario ?? 0) * item.QtdPeca
            };
        }
    }
}