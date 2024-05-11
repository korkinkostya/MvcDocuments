using MvcDocuments.Domain.Entities;

namespace MvcDocuments.Domain.Interfaces;

public interface IDocumentRepository
{
    Task<Document?> GetByIdAsync(int id);
    Task<IEnumerable<Document>> GetAllAsync();

    Task AddAsync(Document document);
    
    Task UpdateAsync(Document document);
    
    Task Remove(Document? document);

    Task<bool> DocumentExistsAsync(int id);

}