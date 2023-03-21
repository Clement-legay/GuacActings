using guacactings.Models;

namespace guacactings.Services;

public interface IDocumentService
{
    // Get all documents
    Task<IEnumerable<Document>> GetDocuments(int page, int rows);
}