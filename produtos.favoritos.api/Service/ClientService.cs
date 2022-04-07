using AutoMapper;
using Domain.DTOs.Client;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ClientDTO> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            return _mapper.Map<ClientDTO>(entity) ?? new ClientDTO();
        }

        public async Task<IEnumerable<ClientDTO>> GetAll()
        {
            var listEntity = await _repository.AllSelectAsync();
            return _mapper.Map<IEnumerable<ClientDTO>>(listEntity);
        }

        public async Task<ClientCreateResultDTO> Post(ClientCreateDTO client)
        {
            var entity = _mapper.Map<ClientEntity>(client);
            var result = await _repository.InsertAsync(entity);

            return _mapper.Map<ClientCreateResultDTO>(result);
        }

        public async Task<ClientUpdateResultDTO> Put(ClientUpdateDTO client)
        {
            var entity = _mapper.Map<ClientEntity>(client);
            var result = await _repository.UpdateAsync(entity);

            return _mapper.Map<ClientUpdateResultDTO>(result);
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
