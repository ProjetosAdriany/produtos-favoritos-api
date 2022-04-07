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
    public class UserRepository : IUserRepository
    {
        protected readonly MyContext _context;
        private readonly DbSet<UserEntity> _dataset;

        public UserRepository(MyContext context)
        {
            _context = context;
            _dataset = context.Set<UserEntity>();
        }
        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            return await _dataset.FirstOrDefaultAsync(u => u.Email.Equals(email));            
        }
    }
}
