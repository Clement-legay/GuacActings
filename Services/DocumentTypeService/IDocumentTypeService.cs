using guacactings.Models;

namespace guacactings.Services;

public interface IDocumentTypeService
{
    // Get all Document Types
    Task<IEnumerable<DocumentType>> GetDocumentTypes(int page, int rows);
    
    // Add a new Document type
    Task<DocumentType?> AddDocumentType(DocumentTypeRegistryDto documentType);
}