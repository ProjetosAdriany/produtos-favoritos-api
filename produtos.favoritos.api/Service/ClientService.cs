using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Client;
using Domain.DTOs.Product;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Provider;
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
        private readonly IClientProductRepository _repositoryClientProduct;
        private readonly IMagaluProvider _magaluProvider;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository repository, 
                IClientProductRepository clientProductRepository, 
                IMapper mapper, 
                IMagaluProvider magaluProvider)
        {
            _repository = repository;
            _repositoryClientProduct = clientProductRepository;
            _mapper = mapper;
            _magaluProvider = magaluProvider;
        }

        public async Task<ClientDTO> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);
            List<ProductEntity> listProductEntity = await _repositoryClientProduct.SelectAsync(id);
            if(entity != null)
            {
                entity.Product = new List<ProductEntity>();

                foreach (ProductEntity item in listProductEntity)
                {
                    ProductEntity searchProduct = await _magaluProvider.GetAsync(item.Id);
                    entity.Product.Add(searchProduct);
                }

                return _mapper.Map<ClientDTO>(entity) ?? new ClientDTO();
            }
            return null;
        }

        public async Task<IEnumerable<ClientDTO>> GetAll()
        {
            List<ClientEntity> listClientEntity = await _repository.AllSelectAsync();

            foreach(ClientEntity client in listClientEntity)
            {
                List<ProductEntity> listProductEntity = await _repositoryClientProduct.SelectAsync(client.Id);                
                client.Product = new List<ProductEntity>();

                foreach (ProductEntity item in listProductEntity)
                {
                    ProductEntity searchProduct = await _magaluProvider.GetAsync(item.Id);                    
                    client.Product.Add(searchProduct);
                }
            }

            return _mapper.Map<IEnumerable<ClientDTO>>(listClientEntity);
        }

        public async Task<ClientCreateResultDTO> Post(ClientCreateDTO client)
        {
            bool searchClient = await _repository.ExistAsync(client.Email);
            if(!searchClient)
            {
                var entity = _mapper.Map<ClientEntity>(client);
                var result = await _repository.InsertAsync(entity);

                if (client.Products.ToList().Count > 0)
                {
                    foreach (ProductCreateDTO item in client.Products)
                    {
                        ProductEntity searchProduct = await _magaluProvider.GetAsync(item.Id);
                        if (searchProduct != null)
                        {
                            var product = _mapper.Map<ProductEntity>(item);
                            await _repositoryClientProduct.InsertAsync(product, result.Id);                    
                        }
                    }
                }

                return _mapper.Map<ClientCreateResultDTO>(result);
            }
            else
            {
                return null;
            }
        }

        public async Task<ClientUpdateResultDTO> Put(ClientUpdateDTO client)
        {
            var entity = _mapper.Map<ClientEntity>(client);
            var result = await _repository.UpdateAsync(entity);            
            await _repositoryClientProduct.DeleteAsync(entity.Id);

            if (client.Products.ToList().Count > 0)
            {
                foreach (ProductCreateDTO item in client.Products)
                {
                    ProductEntity searchProduct = await _magaluProvider.GetAsync(item.Id);
                    if (searchProduct != null)
                    {
                        var product = _mapper.Map<ProductEntity>(item);
                        await _repositoryClientProduct.InsertAsync(product, result.Id);
                    }
                }
            }

            return _mapper.Map<ClientUpdateResultDTO>(result);
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
