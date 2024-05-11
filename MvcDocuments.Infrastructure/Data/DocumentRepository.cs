using System.Data;
using Microsoft.EntityFrameworkCore;
using MvcDocuments.Data.Persistence;
using MvcDocuments.Domain.Entities;
using MvcDocuments.Domain.Interfaces;
using Npgsql;
using NpgsqlTypes;


namespace MvcDocuments.Data.Data;

public class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _context;
    
    private readonly IDocumentRowRepository _documentRowRepository;

    public DocumentRepository(ApplicationDbContext context, IDocumentRowRepository documentRowRepository)
    {
        _context = context;

        _documentRowRepository = documentRowRepository;
    }
    
    public async Task<Document> GetByIdAsync(int id)
    {
        var document =  await _context.Documents.SingleOrDefaultAsync(m => m.Id == id);

        if (document != null)
        {
            document.Rows = (await _documentRowRepository.GetByDocumentIdAsync(document.Id)).ToList();;

            document.TotalPrice = await CalculateTotalPriceFromXmlAsync(document.ToXml());

            
        }
        
        return document;
    }

    public async Task<IEnumerable<Document?>> GetAllAsync()
    {

        var lst = await _context.Documents.ToListAsync();


        foreach (var document in lst)
        {
            document.Rows = (await _documentRowRepository.GetByDocumentIdAsync(document.Id)).ToList();;

            document.TotalPrice = await CalculateTotalPriceFromXmlAsync(document.ToXml());


        }
        
        return await _context.Documents.OrderBy(x=>x.Id).ToListAsync();
    }


    public async Task AddAsync(Document document)
    {
        _context.Add(document);

        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Document document)
    {
        _context.Update(document);
        await _context.SaveChangesAsync();
    }

    public  async Task Remove(Document? document)
    {
        if (document == null)
        {
            return;
        }

        foreach (var documentRow in document.Rows)
        {
            _context.DocumentRows.Remove(documentRow);
        }
        
        _context.Documents.Remove(document);
        
        await _context.SaveChangesAsync();
        
    }


    public async Task<bool> DocumentExistsAsync(int id)
    {
        return await _context.Documents.AnyAsync(e => e.Id == id);
    }



    private async Task<decimal?> CalculateTotalPriceFromXmlAsync(string xmlData)
    {


        if (_context == null)
        {
            return 0;
        }

        var xmlDataParameter = new NpgsqlParameter("@xml_data", NpgsqlDbType.Xml)
        {
            Value = xmlData
        };

        var totalParameter = new NpgsqlParameter("@total", NpgsqlDbType.Integer)
        {
            Direction = ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync("SELECT calculate_total_price(@xml_data)", xmlDataParameter, totalParameter);

        decimal? result = (decimal?)totalParameter.Value;


        return result;
    }
}