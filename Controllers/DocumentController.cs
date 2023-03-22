using System.Net.Mime;
using guacactings.Models;
using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    #region Fields

    private readonly IDocumentService _documentService;

    #endregion
    
    #region Constructor

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    
    #endregion

    #region Methods

    // Get all documents
    [HttpGet(Name = "GetAllDocuments")]
    public async Task<IActionResult> GetDocuments(int page = 1, int rows = 10)
    {
        var result = await _documentService.GetDocuments(page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpGet("{id:int}", Name = "GetDocumentById")]
    public async Task<IActionResult> GetDocumentById(int id)
    {
        var result = await _documentService.GetDocumentById(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpGet("files/{employee}/{docType}/{fileName}", Name = "GetDocumentFile")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> GetDocumentFile(string employee, string docType, string fileName)
    {
        var document = await _documentService.GetDocumentByLink(employee, docType, fileName);
        if (document is null)
        {
            return BadRequest("Document not found");
        }
        
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), document.Link!);

        var contentDisposition = new ContentDisposition
        {
            Inline = true,
            FileName = document.Name + "." + document.ContentType!.Split("/")[1]
        };
        Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
        return PhysicalFile(filePath, document.ContentType!);
    }

    [HttpPost(Name = "AddDocument")]
    public async Task<IActionResult> AddDocument([FromForm] DocumentRegistryDto document)
    {
        var result = await _documentService.AddDocument(document);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    #endregion
}