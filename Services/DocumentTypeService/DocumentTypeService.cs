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
    
    // Add a new Document Type
    public async Task<DocumentType?> AddDocumentType(DocumentTypeRegistryDto documentType)
    {
        var newDocumentType = new DocumentType
        {
            Name = documentType.Name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        var addedDocumentType = _context.DocumentTypes.Add(newDocumentType).Entity;
        await _context.SaveChangesAsync();
        return addedDocumentType;
    }

    #endregion
}