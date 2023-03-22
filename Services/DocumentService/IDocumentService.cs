using System.Net.Mime;
using guacactings.Models;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Services;

public interface IDocumentService
{
    // Get all documents
    public Task<IEnumerable<Document>?> GetDocuments(int page, int rows);

    // Get document by id
    public Task<Document?> GetDocumentById(int id);

    // Add document
    public Task<Document?> AddDocument(DocumentRegistryDto document);
    
    // Get Document File
    public Task<Document?> GetDocumentByLink(string employee, string docType, string fileName);
}