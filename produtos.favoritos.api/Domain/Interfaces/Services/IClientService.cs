using Domain.DTOs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<ClientDTO> Get(Guid id);
        Task<IEnumerable<ClientDTO>> GetAll();
        Task<ClientCreateResultDTO> Post(ClientCreateDTO client);
        Task<ClientUpdateResultDTO> Put(ClientUpdateDTO client);
        Task<bool> Delete(Guid id);
    }
}
