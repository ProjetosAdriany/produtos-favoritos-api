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
    public class ClientRepository : IClientRepository
    {
        protected readonly MyContext _context;
        private readonly DbSet<ClientEntity> _dataset;

        public ClientRepository(MyContext context)
        {
            _context = context;
            _dataset = _context.Set<ClientEntity>();
        }

        public async Task<bool> ExistAsync(string email)
        {
            return await _dataset.AnyAsync(p => p.Email.Equals(email));
        }


        public async Task<ClientEntity> SelectAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ClientEntity>> AllSelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ClientEntity> InsertAsync(ClientEntity client)
        {
            try
            {
                if (client.Id == Guid.Empty)
                {
                    client.Id = Guid.NewGuid();
                }
                client.CreateAt = DateTime.UtcNow;
                client.UpdateAt = DateTime.UtcNow;

                _dataset.Add(client);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return client;
        }


        public async Task<ClientEntity> UpdateAsync(ClientEntity client)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(client.Id));
                if (result == null)
                    return null;

                client.CreateAt = result.CreateAt;
                client.UpdateAt = DateTime.UtcNow;

                _context.Entry(result).CurrentValues.SetValues(client);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return client;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                    return false;

                _dataset.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
