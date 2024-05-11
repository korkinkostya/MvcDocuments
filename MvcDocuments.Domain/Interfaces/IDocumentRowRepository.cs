using MvcDocuments.Domain.Entities;

namespace MvcDocuments.Domain.Interfaces;

public interface IDocumentRowRepository
{
    Task<DocumentRow?> GetByIdAsync(int id);
    Task<IEnumerable<DocumentRow>> GetAllAsync();

    Task<IEnumerable<DocumentRow>> GetByDocumentIdAsync(int documentId);
    
    Task AddAsync(DocumentRow documentRow);
    
    Task UpdateAsync(DocumentRow documentRow);
    
    Task<bool> DocumentRowExistsAsync(int id);
    
    Task Remove(DocumentRow? documentRow);
}