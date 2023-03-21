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

    #endregion
}