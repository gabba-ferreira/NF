using System.Linq;
using NF.DTOs.OrdemServico;
using NF.DTOs.OrdemServico_Peca;
using NF.Models;
using NF.Repositories;
using NF.Repositories.Interfaces;
using NF.Services.Interfaces;

namespace NF.Services
{
    public class OrdemServicoService : IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IOrdemServicoPecaRepository _osPecaRepository;
        private readonly IPecaRepository _pecaRepository;

        public OrdemServicoService(
            IOrdemServicoRepository repository,
            IClienteRepository clienteRepository,
            IVeiculoRepository veiculoRepository,
            IOrdemServicoPecaRepository osPecaRepository,
            IPecaRepository pecaRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _veiculoRepository = veiculoRepository;
            _osPecaRepository = osPecaRepository;
            _pecaRepository = pecaRepository;
        }

        public async Task<List<OrdemServicoResponseDTO>> GetAll()
        {
            var ordens = await _repository.GetAll();
            return ordens.Select(o => MapToResponse(o)).ToList();
        }

        public async Task<OrdemServicoResponseDTO?> GetById(int id)
        {
            var ordem = await _repository.GetByIdWithDetails(id);
            if (ordem == null) return null;
            return MapToResponse(ordem);
        }

        public async Task<List<OrdemServicoResponseDTO>> GetByCliente(int idCliente)
        {
            var ordens = await _repository.GetByCliente(idCliente);
            return ordens.Select(o => MapToResponse(o)).ToList();
        }

        public async Task<List<OrdemServicoResponseDTO>> GetByVeiculo(int idVeiculo)
        {
            var ordens = await _repository.GetByVeiculo(idVeiculo);
            return ordens.Select(o => MapToResponse(o)).ToList();
        }

        public async Task<List<OrdemServicoResponseDTO>> GetByStatus(Status status)
        {
            var ordens = await _repository.GetByStatus(status);
            return ordens.Select(o => MapToResponse(o)).ToList();
        }

        public async Task<OrdemServicoResponseDTO> Create(OrdemServicoRequestDTO dto)
        {
            var cliente = await _clienteRepository.GetById(dto.IdCliente);
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            var veiculo = await _veiculoRepository.GetById(dto.IdVeiculo);
            if (veiculo == null)
                throw new Exception("Veículo não encontrado.");

            if (veiculo.IdCliente != dto.IdCliente)
                throw new Exception("Veículo não pertence ao cliente informado.");

            var ordem = new OrdemServico
            {
                IdCliente = dto.IdCliente,
                IdVeiculo = dto.IdVeiculo,
                Status = dto.Status,
                Responsavel = dto.Responsavel,
                DefeitoDesc = dto.DefeitoDesc,
                ValorMaoDeObra = dto.ValorMaoDeObra,
                DtVisita = dto.DtVisita,
                DtFim = dto.DtFim
            };

            await _repository.Add(ordem);
            return MapToResponse(ordem);
        }

        public async Task<OrdemServicoResponseDTO> CreateComPeca(OrdemServicoRequestDTO dto)
        {
            var cliente = await _clienteRepository.GetById(dto.IdCliente);
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            var veiculo = await _veiculoRepository.GetById(dto.IdVeiculo);
            if (veiculo == null)
                throw new Exception("Veículo não encontrado.");

            if (veiculo.IdCliente != dto.IdCliente)
                throw new Exception("Veículo não pertence ao cliente informado.");


            var ordem = new OrdemServico
            {
                IdCliente = dto.IdCliente,
                IdVeiculo = dto.IdVeiculo,
                Status = dto.Status,
                Responsavel = dto.Responsavel,
                DefeitoDesc = dto.DefeitoDesc,
                ValorMaoDeObra = dto.ValorMaoDeObra,
                DtVisita = dto.DtVisita,
                DtFim = dto.DtFim
            };

            await _repository.Add(ordem);

            foreach (var pecaDto in dto.Pecas)
            {
                var peca = await _pecaRepository.GetById(pecaDto.IdPeca);
                var item = new OrdemServico_Peca
                {
                    IdOs = ordem.IdOs,
                    IdPeca = pecaDto.IdPeca,
                    QtdPeca = pecaDto.QtdPeca,
                };

                await _osPecaRepository.Add(item);

            }

            return MapToResponse(ordem);
        }

        public async Task<OrdemServicoResponseDTO?> Update(int id, OrdemServicoRequestDTO dto)
        {
            var ordem = await _repository.GetById(id);
            if (ordem == null) return null;

            if (ordem.Status == Status.Finalizado || ordem.Status == Status.Cancelado)
                throw new Exception("Não é possível editar uma OS finalizada ou cancelada.");

            var cliente = await _clienteRepository.GetById(dto.IdCliente);
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            var veiculo = await _veiculoRepository.GetById(dto.IdVeiculo);
            if (veiculo == null)
                throw new Exception("Veículo não encontrado.");

            if (veiculo.IdCliente != dto.IdCliente)
                throw new Exception("Veículo não pertence ao cliente informado.");

            ordem.IdCliente = dto.IdCliente;
            ordem.IdVeiculo = dto.IdVeiculo;
            ordem.Status = dto.Status;
            ordem.Responsavel = dto.Responsavel;
            ordem.DefeitoDesc = dto.DefeitoDesc;
            ordem.ValorMaoDeObra = dto.ValorMaoDeObra;
            ordem.DtVisita = dto.DtVisita;
            ordem.DtFim = dto.DtFim;

            await _repository.Update(ordem);


            //OsPecaSync(id, dto);

            var pecasAtuais = await _osPecaRepository.GetByOs(ordem.IdOs);
            var idsDto = dto.Pecas.Select(p => p.IdPeca).ToList();

            foreach (var peca in pecasAtuais)
            {
                if (!idsDto.Contains(peca.IdPeca))
                {
                    await _osPecaRepository.Delete(ordem.IdOs, peca.IdPeca);
                }
            }

            foreach (var pecaDto in dto.Pecas)
            {
                var pecaExistente = pecasAtuais.FirstOrDefault(x => x.IdPeca == pecaDto.IdPeca);

                if (pecaExistente != null)
                {
                    pecaExistente.QtdPeca = pecaDto.QtdPeca;
                    await _osPecaRepository.Update(pecaExistente);
                }
                else
                {
                    var item = new OrdemServico_Peca
                    {
                        IdOs = ordem.IdOs,
                        IdPeca = pecaDto.IdPeca,
                        QtdPeca = pecaDto.QtdPeca
                    };
                    await _osPecaRepository.Update(item);
                }


            }
            return MapToResponse(ordem);
        }

        public async Task<OrdemServicoResponseDTO?> UpdateStatus(int id, Status status)
        {
            var ordem = await _repository.GetById(id);
            if (ordem == null) return null;

            if (ordem.Status == Status.Finalizado || ordem.Status == Status.Cancelado)
                throw new Exception("Não é possível alterar o status de uma OS finalizada ou cancelada.");

            ordem.Status = status;
            await _repository.Update(ordem);
            return MapToResponse(ordem);
        }

        public async Task<bool> Delete(int id)
        {
            var ordem = await _repository.GetById(id);
            if (ordem == null) return false;

            if (ordem.Status != Status.Aberto)
                throw new Exception("Só é possível excluir uma OS com status Aberto.");

            await _repository.Delete(id);
            return true;
        }

        private OrdemServicoResponseDTO MapToResponse(OrdemServico ordem)
        {
            return new OrdemServicoResponseDTO
            {
                IdOs = ordem.IdOs,
                IdCliente = ordem.IdCliente,
                NomeCliente = ordem.Cliente?.RazaoSocial ?? string.Empty,
                IdVeiculo = ordem.IdVeiculo,
                ModeloVeiculo = ordem.Veiculo?.Modelo ?? string.Empty,
                PlacaVeiculo = ordem.Veiculo?.PlacaVeiculo ?? string.Empty,
                Status = ordem.Status,
                Responsavel = ordem.Responsavel,
                DefeitoDesc = ordem.DefeitoDesc,
                ValorMaoDeObra = ordem.ValorMaoDeObra,
                DtVisita = ordem.DtVisita,
                DtFim = ordem.DtFim,
                Pecas = ordem.OrdemServico_Pecas.Select(op => new OrdemServicoPecaResponseDTO
                {
                    IdPeca = op.IdPeca,
                    NomePeca = op.Peca?.NomePeca ?? string.Empty,
                    CodPeca = op.Peca?.CodPeca,
                    PrecoUnitario = op.Peca?.PrecoUnitario ?? 0,
                    QtdPeca = op.QtdPeca,
                }).ToList() ?? new List<OrdemServicoPecaResponseDTO>(),

                ValorTotal = ordem.OrdemServico_Pecas.Sum(op => op.QtdPeca * op.Peca?.PrecoUnitario ?? 0)

            };
        }

        private async void OsPecaSync(int idOrdemServico, OrdemServicoRequestDTO dto)
        {
            var pecasAtuais = await _osPecaRepository.GetByOs(idOrdemServico);
            var idsDto = dto.Pecas.Select(p => p.IdPeca).ToList();

            foreach (var peca in pecasAtuais)
            {
                if (!idsDto.Contains(peca.IdPeca))
                {
                    await _osPecaRepository.Delete(idOrdemServico, peca.IdPeca);
                }
            }

            foreach (var pecaDto in dto.Pecas)
            {
                var pecaExistente = pecasAtuais.FirstOrDefault(x => x.IdPeca == pecaDto.IdPeca);

                if (pecaExistente != null)
                {
                    pecaExistente.QtdPeca = pecaDto.QtdPeca;
                    await _osPecaRepository.Update(pecaExistente);
                }
                else
                {
                    var item = new OrdemServico_Peca
                    {
                        IdOs = idOrdemServico,
                        IdPeca = pecaDto.IdPeca,
                        QtdPeca = pecaDto.QtdPeca
                    };
                    await _osPecaRepository.Update(item);
                }
            }
        }
    }
}