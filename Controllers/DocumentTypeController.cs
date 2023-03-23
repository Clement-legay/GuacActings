using guacactings.Models;
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
    
    // Get document type by id
    [HttpGet("{id:int}", Name = "GetDocumentTypeById")]
    public async Task<IActionResult> GetDocumentTypeById(int id)
    {
        var result = await _documentTypeService.GetDocumentTypeById(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    [HttpPost(Name = "AddDocumentType")]
    public async Task<IActionResult> AddDocumentType([FromForm] DocumentTypeRegistryDto documentType)
    {
        var result = await _documentTypeService.AddDocumentType(documentType);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    // Get document type by id
    [HttpPut("{id:int}/update", Name = "UpdateDocumentType")]
    public async Task<IActionResult> UpdateDocumentType([FromForm] DocumentTypeRegistryDto documentType, int id)
    {
        var result = await _documentTypeService.UpdateDocumentType(documentType, id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    // Delete a document type
    [HttpDelete("{id:int}/delete", Name = "DeleteDocumentType")]
    public async Task<IActionResult> DeleteDocumentType(int id)
    {
        var result = await _documentTypeService.DeleteDocumentType(id);
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    

    #endregion
}