using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class DocumentService : IDocumentService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    #region Constructor

    public DocumentService(DataContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    // Get all Documents
    public async Task<IEnumerable<Document>> GetDocuments(int page, int rows)
    {
        var documents = await _context.Documents.ToListAsync();
        var documentsPaged = documents.Skip((page - 1) * rows).Take(rows);
        return documentsPaged;
    }

    #endregion
}