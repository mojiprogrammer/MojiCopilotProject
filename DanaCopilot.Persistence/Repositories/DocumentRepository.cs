using DanaCopilot.Domain.Entities;
using DanaCopilot.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DanaCopilot.Persistence.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DanaAppDbContext _db;

        public DocumentRepository(DanaAppDbContext db)
        {
            _db = db;
        }

        public async Task<long> CreateAsync(Document document)
        {
            await _db.Documents.AddAsync(document);
            await _db.SaveChangesAsync();
            return document.Id;
        }

        public async Task<Document?> GetByIdAsync(long id)
        {
            return await _db.Documents.FindAsync(id);
        }

        public async Task<List<Document>> GetAllAsync()
        {
            return await _db.Documents.ToListAsync();
        }

        public async Task UpdateAsync(Document document)
        {
            _db.Documents.Update(document);
            await _db.SaveChangesAsync();
        }
    }
}
