using Microsoft.EntityFrameworkCore;
using MvcDocuments.Domain.Entities;

namespace MvcDocuments.Data.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    
    }
    


    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentRow> DocumentRows { get; set; }
}