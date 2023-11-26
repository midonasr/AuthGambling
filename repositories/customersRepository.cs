using Microsoft.EntityFrameworkCore;
using AuthGambling.interfaces;
using AuthGambling.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orders.repositories
{
    public class customersRepository : IcustomersRepository
    {
        private readonly db_tables _context;
        private readonly int pageSize=30;
        public customersRepository(db_tables context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

       
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
