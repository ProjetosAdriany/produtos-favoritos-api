using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<ClientEntity> InsertAsync(ClientEntity client);
        Task<ClientEntity> UpdateAsync(ClientEntity client);
        Task<bool> DeleteAsync(Guid id);
        Task<ClientEntity> SelectAsync(Guid id);
        Task<List<ClientEntity>> AllSelectAsync();
        Task<bool> ExistAsync(string email);
    }
}
