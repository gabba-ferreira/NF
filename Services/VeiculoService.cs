using NF.DTOs.Veiculo;
using NF.Models;
using NF.Repositories.Interfaces;
using NF.Services.Interfaces;

namespace NF.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _repository;
        private readonly IClienteRepository _clienteRepository;

        public VeiculoService(IVeiculoRepository repository, IClienteRepository clienteRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
        }

        public async Task<List<VeiculoResponseDTO>> GetAll()
        {
            var veiculos = await _repository.GetAll();
            return veiculos.Select(v => MapToResponse(v)).ToList();
        }

        public async Task<VeiculoResponseDTO?> GetById(int id)
        {
            var veiculo = await _repository.GetById(id);
            if (veiculo == null) return null;
            return MapToResponse(veiculo);
        }

        public async Task<List<VeiculoResponseDTO>> GetByCliente(int idCliente)
        {
            var veiculos = await _repository.GetByCliente(idCliente);
            return veiculos.Select(v => MapToResponse(v)).ToList();
        }

        public async Task<VeiculoResponseDTO?> GetByPlaca(string placa)
        {
            var veiculo = await _repository.GetByPlaca(placa);
            if (veiculo == null) return null;
            return MapToResponse(veiculo);
        }

        public async Task<VeiculoResponseDTO> Create(VeiculoRequestDTO dto)
        {
            var clienteExiste = await _clienteRepository.GetById(dto.IdCliente);
            if (clienteExiste == null)
                throw new Exception("Cliente não encontrado.");

            if (await _repository.PlacaExiste(dto.PlacaVeiculo))
                throw new Exception("Placa já cadastrada.");

            var veiculo = new Veiculo
            {
                IdCliente = dto.IdCliente,
                Modelo = dto.Modelo,
                PlacaVeiculo = dto.PlacaVeiculo,
                Marca = dto.Marca,
                Horimetro = dto.Horimetro
            };

            await _repository.Add(veiculo);
            return MapToResponse(veiculo);
        }

        public async Task<VeiculoResponseDTO?> Update(int id, VeiculoRequestDTO dto)
        {
            var veiculo = await _repository.GetById(id);
            if (veiculo == null) return null;

            var clienteExiste = await _clienteRepository.GetById(dto.IdCliente);
            if (clienteExiste == null)
                throw new Exception("Cliente não encontrado.");

            if (veiculo.PlacaVeiculo != dto.PlacaVeiculo && await _repository.PlacaExiste(dto.PlacaVeiculo))
                throw new Exception("Placa já cadastrada.");

            veiculo.IdCliente = dto.IdCliente;
            veiculo.Modelo = dto.Modelo;
            veiculo.PlacaVeiculo = dto.PlacaVeiculo;
            veiculo.Marca = dto.Marca;
            veiculo.Horimetro = dto.Horimetro;

            await _repository.Update(veiculo);
            return MapToResponse(veiculo);
        }

        public async Task<bool> Delete(int id)
        {
            var veiculo = await _repository.GetById(id);
            if (veiculo == null) return false;

            await _repository.Delete(id);
            return true;
        }

        private VeiculoResponseDTO MapToResponse(Veiculo veiculo)
        {
            return new VeiculoResponseDTO
            {
                IdVeiculo = veiculo.IdVeiculo,
                IdCliente = veiculo.IdCliente,
                NomeCliente = veiculo.Cliente?.RazaoSocial ?? string.Empty,
                Modelo = veiculo.Modelo,
                PlacaVeiculo = veiculo.PlacaVeiculo,
                Marca = veiculo.Marca,
                Horimetro = veiculo.Horimetro
            };
        }
    }
}