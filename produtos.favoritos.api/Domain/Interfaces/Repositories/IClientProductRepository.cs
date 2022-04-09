using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IClientProductRepository
    {
        Task<ProductEntity> InsertAsync(ProductEntity product, Guid idClient);
        Task<List<ProductEntity>> SelectAsync(Guid idClient);
        Task<bool> DeleteAsync(Guid idClient);
    }
}
