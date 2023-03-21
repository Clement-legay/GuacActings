using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class DocumentTypeService : IDocumentTypeService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    #region Constructor

    public DocumentTypeService(DataContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    // Get all document types
    public async Task<IEnumerable<DocumentType>> GetDocumentTypes(int page, int rows)
    {
        var documentTypes = await _context.DocumentTypes.ToListAsync();
        var documentTypesPaged = documentTypes.Skip((page - 1) * rows).Take(rows);
        return documentTypesPaged;
    }

    #endregion
}