using Microsoft.EntityFrameworkCore;
using MvcDocuments.Data.Persistence;
using MvcDocuments.Domain.Entities;
using MvcDocuments.Domain.Interfaces;

namespace MvcDocuments.Data.Data;

public class DocumentRowRepository : IDocumentRowRepository
{
    private readonly ApplicationDbContext _context;

    public DocumentRowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentRow> GetByIdAsync(int id)
    {
        return await _context.DocumentRows.SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<DocumentRow>> GetByDocumentIdAsync(int documentId)
    {
        return await _context.DocumentRows.Where(m => m.DocumentId == documentId).OrderBy(x=>x.Id).ToListAsync();
    }

    public async Task AddAsync(DocumentRow documentRow)
    {
        _context.Add(documentRow);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DocumentRow documentRow)
    {
        _context.Update(documentRow);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DocumentRowExistsAsync(int id)
    {
        return await _context.DocumentRows.AnyAsync(e => e.Id == id);
    }

    public async Task Remove(DocumentRow? documentRow)
    {
        if (documentRow == null)
        {
            return;
        }

        _context.DocumentRows.Remove(documentRow);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<DocumentRow>> GetAllAsync()
    {
        return await _context.DocumentRows.ToListAsync();
    }
}