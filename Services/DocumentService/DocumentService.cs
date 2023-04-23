using System.Net.Mime;
using System.Security.Claims;
using Azure;
using guacactings.Context;
using guacactings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class DocumentService : IDocumentService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion

    #region Constructor

    public DocumentService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
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
    
    // Get documents by employee
    public async Task<IEnumerable<Document>?> GetDocumentsByEmployeeId(int id)
    {
        var documents = await _context.Documents
            .Where(d => d.EmployeeId == id)
            .ToListAsync();
        return documents;
    }
    
    // Add a document
    public async Task<Document?> AddDocument([FromForm] DocumentRegistryDto document)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        var newDocument = await SaveFileToDisk(document);
        if (newDocument is null)
        {
            return null;
        }
        
        newDocument.CreatedBy = adminId;

        var saveDocument = _context.Documents.Add(newDocument).Entity;
        await _context.SaveChangesAsync();
        return saveDocument;
    }
    
    // Update a document
    public async Task<Document?> UpdateDocument([FromForm] DocumentUpdateDto document, int id)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        
        // Get the document to update
        var updateDocument = await _context.Documents.FindAsync(id);
        if (updateDocument is null) return null;

        // Get the current type of the document
        var oldType = await _context.DocumentTypes.FindAsync(updateDocument.DocumentTypeId);
        if (oldType is null) return null;

        // Get the current path of the document
        var oldPath = Path.Combine(Directory.GetCurrentDirectory(), updateDocument.Link!);
        
        updateDocument.Name = document.Name ?? updateDocument.Name;
        updateDocument.Description = document.Description ?? updateDocument.Description;

        if (document.File is not null)
        {
            // Format the new document to the document dto
            DocumentRegistryDto newDocument = new()
            {
                File = document.File,
                DocumentTypeId = updateDocument.DocumentTypeId,
                EmployeeId = updateDocument.EmployeeId,
                Name = updateDocument.Name,
                Description = updateDocument.Description
            };
            
            // Save the new document to disk
            var newPath = await SaveFileToDisk(newDocument);
            if (newPath is null) return null;

            // Delete the old document
            File.Delete(oldPath);
            oldPath = Path.Combine(Directory.GetCurrentDirectory(), newPath.Link!);
            
            //update the document
            updateDocument.Link = newPath.Link;
            updateDocument.ContentType = newPath.ContentType;
        }
        
        if (document.DocumentTypeId is not null && updateDocument.DocumentTypeId != document.DocumentTypeId)
        {
            // Get the new type of the document
            var newType = await _context.DocumentTypes.FindAsync(document.DocumentTypeId);
            if (newType is null) return null;

            // Get the new path of the document
            var newLink = updateDocument.Link!.Replace(oldType.Name!, newType.Name);
            var newPath = Path.Combine(Directory.GetCurrentDirectory(), newLink);

            // Create the new directory if it doesn't exist
            if (!Directory.Exists(Path.GetDirectoryName(newPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);
            }

            // Move the file to the new directory
            File.Move(oldPath, newPath);

            // Update the document
            updateDocument.DocumentTypeId = document.DocumentTypeId;
            updateDocument.Link = newLink;
        }
        
        updateDocument.UpdatedAt = DateTime.Now;
        updateDocument.UpdatedBy = adminId;
        
        if (Directory.GetFiles(Path.GetDirectoryName(oldPath)!).Length == 0)
        {
            Directory.Delete(Path.GetDirectoryName(oldPath)!);
        }
        
        _context.Documents.Update(updateDocument);
        await _context.SaveChangesAsync();
        return updateDocument;
    }
    
    // Delete a document
    public async Task<Document?> DeleteDocument(int id)
    {
        var document = await _context.Documents.FindAsync(id);
        if (document is null)
        {
            return null;
        }
        
        var link = document.Link;
        var path = Path.Combine(Directory.GetCurrentDirectory(), link!);
        File.Delete(path);
        if (Directory.GetFiles(Path.GetDirectoryName(path)!).Length == 0)
        {
            Directory.Delete(Path.GetDirectoryName(path)!);
        }
        
        _context.Documents.Remove(document);
        await _context.SaveChangesAsync();
        return document;
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

        var contentType = file.ContentType;
        if (contentType == null)
        {
            var provider = new FileExtensionContentTypeProvider();
            provider.TryGetContentType(file.FileName, out var contentTypeProvided);
            contentType = contentTypeProvided;
        }
        
        var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        var link = Path.Combine("files", employee.Username!, documentType.Name!, uniqueFileName);
        
        var newDocument = new Document {
            Name = fileName,
            Link = link,
            ContentType = contentType,
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