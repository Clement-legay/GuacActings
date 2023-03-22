using System.Net.Mime;
using Azure;
using guacactings.Context;
using guacactings.Models;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IEnumerable<Document>?> GetDocuments(int page, int rows)
    {
        var documents = await _context.Documents.ToListAsync();
        var documentsPaged = documents.Skip((page - 1) * rows).Take(rows);
        return documentsPaged;
    }
    
    // Get a document by id
    public async Task<Document?> GetDocumentById(int id)
    {
        var document = await _context.Documents.FindAsync(id);
        return document ?? null;
    }
    
    // Add a document
    public async Task<Document?> AddDocument(DocumentRegistryDto document)
    {
        var newDocument = await SaveFileToDisk(document);
        if (newDocument is null)
        {
            return null;
        }
        
        var saveDocument = _context.Documents.Add(newDocument).Entity;
        await _context.SaveChangesAsync();
        return saveDocument;
    }
    
    // Get Document File
    public async Task<Document?> GetDocumentByLink(string employee, string docType, string fileName)
    {
        var fileUrl = Path.Combine("files", employee, docType, fileName);
        
        var document = await _context.Documents.FirstOrDefaultAsync(d => d.Link == fileUrl);

        return document ?? null;
    }

    private async Task<Document?> SaveFileToDisk(DocumentRegistryDto document)
    {
        var documentType = await _context.DocumentTypes.FindAsync(document.DocumentTypeId);
        var employee = await _context.Employees.FindAsync(document.EmployeeId);
        var file = document.File;
        
        if (file is null || documentType is null || employee is null)
        {
            return null;
        }
        
        var fileName = document.Name ?? file.FileName;
        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "files" , employee.Username!, documentType.Name!, uniqueFileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        var link = Path.Combine("files", employee.Username!, documentType.Name!, uniqueFileName);
        
        var newDocument = new Document {
            Name = fileName,
            Link = link,
            ContentType = file.ContentType,
            Description = document.Description,
            DocumentTypeId = document.DocumentTypeId,
            EmployeeId = document.EmployeeId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
        
        return newDocument;
    }

    #endregion
}