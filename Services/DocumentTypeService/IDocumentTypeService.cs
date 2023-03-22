using guacactings.Models;

namespace guacactings.Services;

public interface IDocumentTypeService
{
    // Get all Document Types
    Task<IEnumerable<DocumentType>> GetDocumentTypes(int page, int rows);
    
    // Get a document type
    Task<DocumentType?> GetDocumentTypeById(int id);

    // Add a new Document type
    Task<DocumentType?> AddDocumentType(DocumentTypeRegistryDto documentType);
    
    // Update a document type
    Task<DocumentType?> UpdateDocumentType(DocumentTypeRegistryDto documentType, int id);
    
    // Delete a document type
    Task<DocumentType?> DeleteDocumentType(int id);
}