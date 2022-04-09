using Data.Context;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ClientProductRepository : IClientProductRepository
    {
            protected readonly MyContext _context;
            private readonly DbSet<ClientProductEntity> _dataset;

            public ClientProductRepository(MyContext context)
            {
                _context = context;
                _dataset = _context.Set<ClientProductEntity>();
            }

            public async Task<List<ProductEntity>> SelectAsync(Guid idClient)
            {
                try
                {
                    List<ClientProductEntity> listClientProductEntity = await _dataset.Where(p => p.IdClient.Equals(idClient)).ToListAsync();
                    
                    List<ProductEntity> ListProductEntity = new();
                    foreach(ClientProductEntity item in listClientProductEntity)
                    {
                        ProductEntity productEntity = new()
                        {
                            Id = item.IdProduct
                        };                        
                        ListProductEntity.Add(productEntity);
                    }

                    return ListProductEntity;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }

            public async Task<ProductEntity> InsertAsync(ProductEntity product, Guid idClient)
            {
                try
                {
                    ClientProductEntity result = await _dataset.SingleOrDefaultAsync(p => p.IdProduct.Equals(product.Id) && p.IdClient.Equals(idClient));
                    if(result == null)
                    {
                        ClientProductEntity clientProductEntity = new()
                        {
                            IdClient = idClient,
                            IdProduct = product.Id

                        };
                        if (clientProductEntity.Id == Guid.Empty)
                        {
                            clientProductEntity.Id = Guid.NewGuid();
                        }
                    
                        _dataset.Add(clientProductEntity);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }

            return product;
            }

            public async Task<bool> DeleteAsync(Guid idClient)
            {
                try
                {
                    List<ClientProductEntity> result = await _dataset.Where(p => p.IdClient.Equals(idClient)).ToListAsync();
                    if (result == null)
                        return false;

                    foreach(var item in result)
                    {
                        _dataset.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }

        }
        }
}
