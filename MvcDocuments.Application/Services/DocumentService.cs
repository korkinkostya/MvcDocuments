using MvcDocuments.Domain.Entities;
using MvcDocuments.Domain.Interfaces;

namespace MvcDocuments.Application.Services;

public class DocumentService
{
    private readonly IDocumentRepository _documentRepository;
    
    private readonly IDocumentRowRepository _documentRowRepository;

    public DocumentService(IDocumentRepository documentRepository, IDocumentRowRepository documentRowRepository)
    {
        _documentRepository = documentRepository;
        _documentRowRepository = documentRowRepository;
    }

    public async Task<IEnumerable<Document?>> GetAllDocumentsAsync()
    {
        return await _documentRepository.GetAllAsync();
    }
}