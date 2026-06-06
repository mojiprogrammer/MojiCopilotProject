using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DanaCopilot.Persistence
{
    
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly DanaAppDbContext _db;

        public BaseRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
        }

        public async Task<TEntity?> GetByIdAsync(long id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }
    }
}
