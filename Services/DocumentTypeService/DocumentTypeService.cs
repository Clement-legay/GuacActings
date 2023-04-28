using System.Security.Claims;
using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class DocumentTypeService : IDocumentTypeService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion

    #region Constructor

    public DocumentTypeService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
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
    
    // Get document type by id
    public async Task<DocumentType?> GetDocumentTypeById(int id)
    {
        var documentType = await _context.DocumentTypes.FindAsync(id);
        return documentType ?? null;
    }
    
    // Add a new Document Type
    public async Task<DocumentType?> AddDocumentType(DocumentTypeRegistryDto documentType)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        var newDocumentType = new DocumentType
        {
            Name = documentType.Name,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatedBy = adminId
        };

        var addedDocumentType = _context.DocumentTypes.Add(newDocumentType).Entity;
        await _context.SaveChangesAsync();
        return addedDocumentType;
    }
    
    // Update a document type
    public async Task<DocumentType?> UpdateDocumentType(DocumentTypeRegistryDto documentType, int id)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        var updateDocumentType = await _context.DocumentTypes.Include(d => d.Documents).FirstOrDefaultAsync(d => d.Id == id);
        if (updateDocumentType is null)
        {
            return null;
        }
        
        // if documentType.Documents is not null, change directory name and update all documents link
        if (updateDocumentType.Documents!.Count != 0)
        {
            var oldDirectoryName = updateDocumentType.Name;
            var newDirectoryName = documentType.Name;
            
            if (oldDirectoryName != newDirectoryName)
            {
            
                var documents = updateDocumentType.Documents;
                foreach (var document in documents)
                {
                    var oldPath = document.Link;
                    var newPath = oldPath!.Replace(oldDirectoryName!, newDirectoryName);
                    
                    var oldDirectory = Path.Combine(Directory.GetCurrentDirectory(), oldPath);
                    var newDirectory = Path.Combine(Directory.GetCurrentDirectory(), newPath);
                    
                    // test directory exists from path
                    if (Directory.Exists(Path.GetDirectoryName(oldDirectory)))
                    {
                        Directory.Move(Path.GetDirectoryName(oldDirectory)!, Path.GetDirectoryName(newDirectory)!);
                    }

                    if (File.Exists(newPath))
                    {
                        document.Link = newPath;
                        document.UpdatedAt = DateTime.Now;
                    }

                    _context.Documents.Update(document);
                }
            }
        }

        updateDocumentType.Name = documentType.Name;
        updateDocumentType.UpdatedAt = DateTime.Now;
        updateDocumentType.UpdatedBy = adminId;
        
        _context.DocumentTypes.Update(updateDocumentType);
        await _context.SaveChangesAsync();
        return updateDocumentType;
    }
    
    // Delete a document type
    public async Task<DocumentType?> DeleteDocumentType(int id)
    {
        var documentType = await _context.DocumentTypes.Include(d => d.Documents).FirstOrDefaultAsync(d => d.Id == id);
        if (documentType is null)
        {
            return null;
        }

        if (documentType.Documents!.Count != 0) return null;

        _context.DocumentTypes.Remove(documentType);
        await _context.SaveChangesAsync();
        return documentType;

    }

    #endregion
}