using guacactings.Services;
using Microsoft.AspNetCore.Mvc;

namespace guacactings.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentTypeController : ControllerBase
{
    #region Fields

    private readonly IDocumentTypeService _documentTypeService;

    #endregion
    
    #region Constructor

    public DocumentTypeController(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }
    
    #endregion

    #region Methods
    
    // Get all document types
    [HttpGet(Name = "GetAllDocumentTypes")]
    public async Task<IActionResult> GetDocumentTypes(int page = 1, int rows = 10)
    {
        var result = await _documentTypeService.GetDocumentTypes(page, rows);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    #endregion
}