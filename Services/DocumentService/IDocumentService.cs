using System.Net.Mime;
using guacactings.Models;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Services;

public interface IDocumentService
{
    // Get all documents
    Task<IEnumerable<Document>?> GetDocuments(int page, int rows);

    // Get document by id
    Task<Document?> GetDocumentById(int id);

    // Get documents by employee
    Task<IEnumerable<Document>?> GetDocumentsByEmployeeId(int id);
    
    // Add document
    Task<Document?> AddDocument(DocumentRegistryDto document);
    
    // Get Document File
    Task<Document?> GetDocumentByLink(string employee, string docType, string fileName);
    
    // Update a document
    Task<Document?> UpdateDocument(DocumentUpdateDto document, int id);
    
    // Delete a document
    Task<Document?> DeleteDocument(int id);
}