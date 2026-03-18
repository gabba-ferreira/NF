using NF.DTOs.Cliente;
using NF.Models;
using NF.Repositories.Interfaces;
using NF.Services.Interfaces;

namespace NF.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClienteResponseDTO>> GetAll()
        {
            var clientes = await _repository.GetAll();
            return clientes.Select(c => MapToResponse(c)).ToList();
        }

        public async Task<ClienteResponseDTO?> GetById(int id)
        {
            var cliente = await _repository.GetById(id);
            if (cliente == null) return null;
            return MapToResponse(cliente);
        }

        public async Task<List<ClienteResponseDTO>> GetByRazaoSocial(string razaoSocial)
        {
            var clientes = await _repository.GetByRazaoSocial(razaoSocial);
            return clientes.Select(c => MapToResponse(c)).ToList();
        }

        public async Task<ClienteResponseDTO> Create(ClienteRequestDTO dto)
        {
            if (await _repository.CNPJExiste(dto.CNPJ))
                throw new Exception("CNPJ já cadastrado.");

            var cliente = new Cliente
            {
                RazaoSocial = dto.RazaoSocial,
                Email = dto.Email,
                CNPJ = dto.CNPJ,
                Telefone = dto.Telefone,
                TelefoneOp = dto.TelefoneOp,
                Endereco = dto.Endereco,
                CEP = dto.CEP
            };

            await _repository.Add(cliente);
            return MapToResponse(cliente);
        }

        public async Task<ClienteResponseDTO?> Update(int id, ClienteRequestDTO dto)
        {
            var cliente = await _repository.GetById(id);
            if (cliente == null) return null;

            if (cliente.CNPJ != dto.CNPJ && await _repository.CNPJExiste(dto.CNPJ))
                throw new Exception("CNPJ já cadastrado.");

            cliente.RazaoSocial = dto.RazaoSocial;
            cliente.Email = dto.Email;
            cliente.CNPJ = dto.CNPJ;
            cliente.Telefone = dto.Telefone;
            cliente.TelefoneOp = dto.TelefoneOp;
            cliente.Endereco = dto.Endereco;
            cliente.CEP = dto.CEP;

            await _repository.Update(cliente);
            return MapToResponse(cliente);
        }

        public async Task<bool> Delete(int id)
        {
            var cliente = await _repository.GetById(id);
            if (cliente == null) return false;

            await _repository.Delete(id);
            return true;
        }

        private ClienteResponseDTO MapToResponse(Cliente cliente)
        {
            return new ClienteResponseDTO
            {
                IdCliente = cliente.IdCliente,
                RazaoSocial = cliente.RazaoSocial,
                Email = cliente.Email,
                CNPJ = cliente.CNPJ,
                Telefone = cliente.Telefone,
                TelefoneOp = cliente.TelefoneOp,
                Endereco = cliente.Endereco,
                CEP = cliente.CEP
            };
        }
    }
}